using System.Data;
using Dapper;
using Npgsql;
using SimtekData.Configurations;
using SimtekData.Models;
using SimtekData.Models.Intervention;
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

    public Task<InterventionShortDto?> GetInterventionByIdAsync(int id,
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
        SUM(im.quantity * m.price) as TotalMaterialCost
        FROM interventions i
            INNER JOIN sites s ON i.site_id = s.id
        INNER JOIN workerinterventions wi ON i.id = wi.intervention_id
        INNER JOIN workers w ON wi.worker_id = w.id
        LEFT JOIN interventionmaterials im ON i.id = im.intervention_id
        LEFT JOIN materials m ON im.material_id = m.id
        WHERE i.id = @Id
        GROUP BY i.id, s.name, i.intervention_date;
";
        
        if (cancellationToken.IsCancellationRequested)
            throw new TaskCanceledException("Cancellation requested in GetShortInterventionByIdAsync");

        using var connection = new NpgsqlConnection(connectionString: _connectionString);
        connection.Open();
        return connection.QuerySingleAsync<InterventionShortDto?>(sql, new { Id = id });
    }

    // public async Task<IEnumerable<FullInterventionDto>> GetFullInterventions(CancellationToken cancellationToken)
    // {
    //     var sql = @"
    //     SELECT
    //         -- intervention
    //         i.id as Id, i.intervention_date as InterventionDate, i.stored as Stored, i.title as Title, i.description as Description,
    //         -- site
    //         s.id AS SiteId, s.name AS SiteName, s.address AS SiteAddress,
    //         -- customer
    //         c.id AS CustomerId, c.name AS CustomerName, c.surname AS CustomerSurname, c.address AS CustomerAddress, c.vat AS CustomerVat, c.email AS CustomerEmail, c.phone_number AS CustomerPhoneNumber,
    //         -- worker
    //         w.id AS WorkerId, w.name AS WorkerName, w.surname AS WorkerSurname, w.pph AS WorkerPph, wi.hours_worked AS HoursWorked,
    //         -- material
    //         m.id AS MaterialId, m.name AS MaterialName, m.price AS MaterialPrice, m.unit AS MaterialUnit, im.quantity AS MaterialQuantity
    //     FROM interventions i
    //     INNER JOIN sites s ON i.site_id = s.id
    //     INNER JOIN customers c ON s.customer_id = c.id
    //     INNER JOIN workerinterventions wi ON i.id = wi.intervention_id
    //     INNER JOIN workers w ON wi.worker_id = w.id
    //     LEFT JOIN interventionmaterials im ON i.id = im.intervention_id
    //     LEFT JOIN materials m ON im.material_id = m.id;
    // ";
    //
    //     await using var connection = new NpgsqlConnection(connectionString: _connectionString);
    //     await connection.OpenAsync(cancellationToken);
    //
    //     return await connection.QueryAsync<FullInterventionDto>(sql,cancellationToken);
    // }
    //
    // public Task<FullInterventionDto?> GetFullInterventionById(int id,
    //     CancellationToken cancellationToken = default)
    // {
    //     var sql = @"
    //     SELECT
    //         -- intervention
    //         i.id as Id, i.intervention_date as InterventionDate, i.stored as Stored, i.title as Title, i.description as Description,
    //         -- site
    //         s.id AS SiteId, s.name AS SiteName, s.address AS SiteAddress,
    //         -- customer
    //         c.id AS CustomerId, c.name AS CustomerName, c.surname AS CustomerSurname, c.address AS CustomerAddress, c.vat AS CustomerVat, c.email AS CustomerEmail, c.phone_number AS CustomerPhoneNumber,
    //         -- worker
    //         w.id AS WorkerId, w.name AS WorkerName, w.surname AS WorkerSurname, w.pph AS WorkerPph, wi.hours_worked AS HoursWorked,
    //         -- material
    //         m.id AS MaterialId, m.name AS MaterialName, m.price AS MaterialPrice, m.unit AS MaterialUnit, im.quantity AS MaterialQuantity
    //     FROM interventions i
    //     INNER JOIN sites s ON i.site_id = s.id
    //     INNER JOIN customers c ON s.customer_id = c.id
    //     INNER JOIN workerinterventions wi ON i.id = wi.intervention_id
    //     INNER JOIN workers w ON wi.worker_id = w.id
    //     LEFT JOIN interventionmaterials im ON i.id = im.intervention_id
    //     LEFT JOIN materials m ON im.material_id = m.id
    //     WHERE i.id = @id;
    // ";
    //
    //     using var connection = new NpgsqlConnection(connectionString: _connectionString);
    //     connection.Open();
    //     if (cancellationToken.IsCancellationRequested)
    //         throw new TaskCanceledException("Cancellation requested in GetFullInterventionByIdAsync");
    //
    //     return connection.QuerySingleAsync<FullInterventionDto?>(sql, new { id });
    // }

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
                await connection.QuerySingleOrDefaultAsync<int>(sql, new { FullCustomerName = fullCustomerName },
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
                    "INSERT INTO Materials (id, name, price, unit, quantity, stored, creation_date, last_update_date) VALUES (@Id, @Name, @Price, @Unit, @Quantity, FALSE, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP) ON CONFLICT (id) DO NOTHING;";
                await connection.ExecuteAsync(sql, material.Material, transaction);

                sql =
                    "INSERT INTO InterventionMaterials (intervention_id, material_id, quantity, stored, creation_date, last_update_date) VALUES (@InterventionId, @MaterialId, @Quantity, FALSE, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);";
                await connection.ExecuteAsync(sql,
                    new
                    {
                        InterventionId = interventionId, MaterialId = material.Material.Id, Quantity = material.Quantity
                    }, transaction);
            }

            // Insert into WorkerInterventions
            foreach (var workerHour in intervention.WorkerHours)
            {
                // Insert worker if it does not exist
                sql =
                    "INSERT INTO Workers (id, name, surname, pph, stored, creation_date, last_update_date) VALUES (@Id, @Name, @Surname, @Pph, FALSE, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP) ON CONFLICT (id) DO NOTHING;";
                await connection.ExecuteAsync(sql, workerHour.Worker, transaction);

                sql =
                    "INSERT INTO WorkerInterventions (worker_id, intervention_id, hours_worked, stored, creation_date, last_update_date) VALUES (@WorkerId, @InterventionId, @HoursWorked, FALSE, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);";
                await connection.ExecuteAsync(sql,
                    new
                    {
                        WorkerId = workerHour.Worker.Id, InterventionId = interventionId,
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