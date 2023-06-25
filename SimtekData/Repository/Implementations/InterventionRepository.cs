using System.Data;
using Dapper;
using Npgsql;
using SimtekData.Configurations;
using SimtekData.Models;
using SimtekData.Models.Intervention;
using SimtekData.Models.Worker;
using SimtekData.Repository.Abstractions;
using SimtekDomain;

namespace SimtekData.Repository.Implementations;

public class InterventionRepository : IInterventionRepository
{
    private readonly string _connectionString;
    

    public InterventionRepository(DbConnectionLiteral connectionString)
    {
        _connectionString = connectionString.ConnectionString;
    }


    public async Task<IEnumerable<InterventionShortDto>> GetInterventionsAsync(CancellationToken cancellationToken)
    {
        var sql = @"
        SELECT
        i.id as Id,
        s.name as SiteName,
        i.intervention_date as InterventionDate,
        i.description as Description,
        i.title as Title,
        SUM(wi.hours_worked) as HoursSpent,
        SUM(wi.hours_worked * w.pph) as TotalWorkerCost,
        SUM(im.quantity * m.price) as TotalMaterialCost
        FROM interventions i
            INNER JOIN sites s ON i.site_id = s.id
        INNER JOIN workerinterventions wi ON i.id = wi.intervention_id
        INNER JOIN workers w ON wi.worker_id = w.id
        LEFT JOIN interventionmaterials im ON i.id = im.intervention_id
        LEFT JOIN materials m ON im.material_id = m.id
        GROUP BY i.id, s.name, i.intervention_date;
";
        await using var connection = new NpgsqlConnection(connectionString: _connectionString);
        await connection.OpenAsync(cancellationToken);
        return await connection.QueryAsync<InterventionShortDto>(sql,cancellationToken);
    }

    public async Task<FullInterventionDto?> GetInterventionByIdAsync(int id,
        CancellationToken cancellationToken = default)
    {
        var sql = @"
        SELECT
        i.id as Id,
        s.name as SiteName,
        i.title as Title,
        i.description as Description,
        i.intervention_date as InterventionDate,
        SUM(wi.hours_worked) as HoursSpent,
        SUM(wi.hours_worked * w.pph) as TotalWorkerCost,
        SUM(im.quantity * m.price) as TotalMaterialCost,
        i.stored as Stored
        FROM interventions i
            INNER JOIN sites s ON i.site_id = s.id
        INNER JOIN workerinterventions wi ON i.id = wi.intervention_id
        INNER JOIN workers w ON wi.worker_id = w.id
        LEFT JOIN interventionmaterials im ON i.id = im.intervention_id
        LEFT JOIN materials m ON im.material_id = m.id
        WHERE i.id = @IdInt
        GROUP BY i.id, s.name, i.intervention_date;
";
        
        if (cancellationToken.IsCancellationRequested)
            throw new TaskCanceledException("Cancellation requested in GetShortInterventionByIdAsync");

        await using var connection = new NpgsqlConnection(connectionString: _connectionString);
        await connection.OpenAsync(cancellationToken);
        var interventionShort =  await connection.QueryFirstOrDefaultAsync<InterventionShortDto>(sql, new {IdInt = id});
        
        if (interventionShort is null) return null;

        var materialsQuery = @"
SELECT
    m.id as Id, 
    m.name as Name, 
    m.price as Price, 
    m.unit as Unit, 
    im.quantity as Quantity, 
    m.stored as Stored, 
    m.creation_date as CreationDate, 
    m.last_update_date as LastUpdateDate
FROM materials m
INNER JOIN interventionmaterials im ON m.id = im.material_id
WHERE im.intervention_id = @IntId;
";

        var siteQuery = @"
SELECT 
    s.id as Id,
    s.name as Name,
    s.address as Address,
    s.customer_id as CustomerId,
    s.stored as Stored,
    s.creation_date as CreationDate,
    s.last_update_date as LastUpdateDate
    FROM sites s
INNER JOIN interventions i ON s.id = i.site_id
WHERE i.id = @Id;
";

        var customerQuery = @"
SELECT * FROM customers c
INNER JOIN sites s ON c.id = s.customer_id
WHERE s.id = @Id;
";

        var workersQuery = @"
SELECT 
   w.id as Id, 
   w.name as Name, 
   w.surname as Surname,
   wi.hours_worked as HourWorked,
   w.pph as Pph 
FROM workers w
INNER JOIN workerinterventions wi ON w.id = wi.worker_id
WHERE wi.intervention_id = @IntId;
";
        
        var materials = (await connection.QueryAsync<MaterialDto>(materialsQuery, new {IntId=id})).ToList();
        var site = (await connection.QuerySingleAsync<SiteDto>(siteQuery, new {Id=interventionShort.Id}));
        var customer = (await connection.QuerySingleAsync<CustomerDto>(customerQuery, new {Id= site.CustomerId}));
        var workers = (await connection.QueryAsync<WorkerHoursProjection>(workersQuery, new { IntId = interventionShort.Id })).ToList();

        var fullIntDto = new FullInterventionDto
        {
            InterventionShortDto = interventionShort ?? throw new ArgumentNullException("interventionShort"),
            MaterialDto = materials,
            SiteDto = site,
            CustomerDto = customer,
            WorkerDto = workers
        };
        return fullIntDto;
    }

    public async Task AddInterventionAsync(Intervention intervention, CancellationToken cancellationToken)
    {
        // Start a transaction to ensure all inserts are successful
        await using var connection = new NpgsqlConnection(connectionString: _connectionString);
        connection.Open();

        await using var transaction = connection.BeginTransaction();
        try
        {
            // Insert customer if it does not exist
            var fullCustomerName = $"{intervention.Site.Customer.Name} {intervention.Site.Customer.Surname}";

            // Check if customer exists
            var sql = "SELECT id FROM Customers WHERE CONCAT(name, ' ', surname) = @FullCustomerName;";
            var customerId =
                await connection.QueryFirstOrDefaultAsync<int>(sql, new { FullCustomerName = fullCustomerName },
                    transaction);

            if (customerId == 0)
            {
                // Insert new customer
                sql =
                    "INSERT INTO Customers (name, surname, vat, email, stored, creation_date, last_update_date) VALUES (@Name, @Surname, @Vat, @Email, FALSE, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP) RETURNING id;";
                customerId = await connection.QuerySingleAsync<int>(sql, intervention.Site.Customer, transaction);
            }

            // Insert site if it does not exist
            sql =
                "INSERT INTO Sites (name, address, customer_id, stored, creation_date, last_update_date) VALUES (@Name, @Address, @CustomerId, FALSE, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP) ON CONFLICT (name) DO NOTHING RETURNING id;";
            var siteId = await connection.QuerySingleOrDefaultAsync<int>(sql,
                new { Name = intervention.Site.Name, Address = intervention.Site.Address, CustomerId = customerId },
                transaction);

            if (siteId == 0)
            {
                sql = "SELECT id FROM Sites WHERE name = @Name;";
                siteId = await connection.QuerySingleAsync<int>(sql, new { Name = intervention.Site.Name },
                    transaction);
            }

            sql =
                "INSERT INTO Interventions (site_id, title, description, intervention_date, stored, creation_date, last_update_date) VALUES (@SiteId, @Title, @Description, @InterventionDate, FALSE, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP) RETURNING id;";
            var interventionId = await connection.QuerySingleAsync<int>(sql,
                new
                {
                    SiteId = siteId, Title = intervention.Title, Description = intervention.Description,
                    InterventionDate = intervention.InterventionDate
                }, transaction);

            // Insert into InterventionMaterials
            foreach (var material in intervention.Materials)
            {
                // Insert material if it does not exist
                sql =
                    "INSERT INTO Materials (name, price, unit, quantity, stored, creation_date, last_update_date) VALUES (@Name, @Price, @Unit, @Quantity, FALSE, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP) ON CONFLICT (name) DO NOTHING;";
                await connection.ExecuteAsync(sql, material.Material, transaction);

                var mat = await connection.QuerySingleAsync<MaterialDto>("SELECT * FROM materials WHERE NAME = @MatName", new{MatName=material.Material.Name});
                
                sql =
                    "INSERT INTO InterventionMaterials (intervention_id, material_id, quantity, stored, creation_date, last_update_date) VALUES (@InterventionId, @MaterialId, @Quantity, FALSE, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);";
                await connection.ExecuteAsync(sql,
                    new
                    {
                        InterventionId = interventionId, MaterialId = mat.Id, Quantity = material.Quantity
                    }, transaction);
            }

            // Insert into WorkerInterventions
            foreach (var workerHour in intervention.WorkerHours)
            {
                // Insert worker if it does not exist
                sql =
                    "INSERT INTO Workers (name, surname, pph, stored, creation_date, last_update_date) VALUES (@Name, @Surname, @Pph, FALSE, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP) ON CONFLICT (name, surname) DO NOTHING;";
                await connection.ExecuteAsync(sql, workerHour.Worker, transaction);

                var wrk = await connection.QuerySingleAsync<WorkerDto>(
                    "SELECT * FROM workers WHERE CONCAT(name,'-',surname) = @NamSur", new{NamSur=$"{workerHour.Worker.Name}-{workerHour.Worker.Surname}"});
                
                sql =
                    "INSERT INTO WorkerInterventions (worker_id, intervention_id, hours_worked, stored, creation_date, last_update_date) VALUES (@WorkerId, @InterventionId, @HoursWorked, FALSE, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);";
                await connection.ExecuteAsync(sql,
                    new
                    {
                        WorkerId = wrk.Id, InterventionId = interventionId,
                        HoursWorked = workerHour.Hours
                    }, transaction);
            }

            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception)
        {
            await transaction.RollbackAsync(cancellationToken);
            throw; // rethrowing the exception to be handled by upper layers
        }
    }

    public void DeleteIntervention(int id)
    {
        using var connection = new NpgsqlConnection(connectionString: _connectionString);
        connection.Open();
        using IDbTransaction? transaction = connection.BeginTransaction();

        try
        {
            var sql = "UPDATE Interventions SET stored = TRUE WHERE id = @id";
            connection.Execute(sql, new { id }, transaction);

            sql = "UPDATE InterventionMaterials SET stored = TRUE WHERE intervention_id = @id";
            connection.Execute(sql, transaction);

            sql = "UPDATE WorkerInterventions SET stored = TRUE WHERE intervention_id = @id";
            connection.Execute(sql, transaction);

            transaction.Commit();
        }
        catch (Exception)
        {
            transaction?.Rollback();
            throw;
        }
    }
}