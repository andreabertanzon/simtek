using System.Data;
using System.Transactions;
using Dapper;
using Npgsql;
using OneOf;
using SimtekData.Models;
using SimtekData.Models.Intervention;
using SimtekDomain;
using SimtekDomain.Errors;

namespace SimtekData.Repository;

public class InterventionRepository
{
    private readonly IDbConnection _db;

    public InterventionRepository(IDbConnection db)
    {
        _db = db;
    }


    public IEnumerable<InterventionShortDto> GetShortInterventions()
    {
        var sql = @"
        SELECT
        i.id as Id,
        s.name as SiteName,
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
        GROUP BY i.id, s.name, i.intervention_date;
";
        try
        {
            _db.Open();
            return _db.Query<InterventionShortDto>(sql);
        }
        finally
        {
            _db.Close();
        }
    }

    public Task<InterventionShortDto?> GetShortInterventionByIdAsync(int id,
        CancellationToken cancellationToken = default)
    {
        var sql = @"
        SELECT
        i.id as Id,
        s.name as SiteName,
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
        try
        {
            if (cancellationToken.IsCancellationRequested)
                throw new TaskCanceledException("Cancellation requested in GetShortInterventionByIdAsync");

            _db.Open();
            return _db.QuerySingleAsync<InterventionShortDto?>(sql, new { Id = id });
        }
        finally
        {
            _db.Close();
        }
    }

    public IEnumerable<FullInterventionDto> GetFullInterventions()
    {
        var sql = @"
        SELECT
            -- intervention
            i.id as Id, i.intervention_date as InterventionDate, i.stored as Stored, i.title as Title, i.description as Description,
            -- site
            s.id AS SiteId, s.name AS SiteName, s.address AS SiteAddress,
            -- customer
            c.id AS CustomerId, c.name AS CustomerName, c.surname AS CustomerSurname, c.address AS CustomerAddress, c.vat AS CustomerVat, c.email AS CustomerEmail, c.phone_number AS CustomerPhoneNumber,
            -- worker
            w.id AS WorkerId, w.name AS WorkerName, w.surname AS WorkerSurname, w.pph AS WorkerPph, wi.hours_worked AS HoursWorked,
            -- material
            m.id AS MaterialId, m.name AS MaterialName, m.price AS MaterialPrice, m.unit AS MaterialUnit, im.quantity AS MaterialQuantity
        FROM interventions i
        INNER JOIN sites s ON i.site_id = s.id
        INNER JOIN customers c ON s.customer_id = c.id
        INNER JOIN workerinterventions wi ON i.id = wi.intervention_id
        INNER JOIN workers w ON wi.worker_id = w.id
        LEFT JOIN interventionmaterials im ON i.id = im.intervention_id
        LEFT JOIN materials m ON im.material_id = m.id;
    ";

        try
        {
            _db.Open();

            return _db.Query<FullInterventionDto>(sql);
        }
        finally
        {
            _db.Close();
        }
    }

    public Task<FullInterventionDto?> GetFullInterventionById(int id,
        CancellationToken cancellationToken = default)
    {
        var sql = @"
        SELECT
            -- intervention
            i.id as Id, i.intervention_date as InterventionDate, i.stored as Stored, i.title as Title, i.description as Description,
            -- site
            s.id AS SiteId, s.name AS SiteName, s.address AS SiteAddress,
            -- customer
            c.id AS CustomerId, c.name AS CustomerName, c.surname AS CustomerSurname, c.address AS CustomerAddress, c.vat AS CustomerVat, c.email AS CustomerEmail, c.phone_number AS CustomerPhoneNumber,
            -- worker
            w.id AS WorkerId, w.name AS WorkerName, w.surname AS WorkerSurname, w.pph AS WorkerPph, wi.hours_worked AS HoursWorked,
            -- material
            m.id AS MaterialId, m.name AS MaterialName, m.price AS MaterialPrice, m.unit AS MaterialUnit, im.quantity AS MaterialQuantity
        FROM interventions i
        INNER JOIN sites s ON i.site_id = s.id
        INNER JOIN customers c ON s.customer_id = c.id
        INNER JOIN workerinterventions wi ON i.id = wi.intervention_id
        INNER JOIN workers w ON wi.worker_id = w.id
        LEFT JOIN interventionmaterials im ON i.id = im.intervention_id
        LEFT JOIN materials m ON im.material_id = m.id
        WHERE i.id = @id;
    ";
        try
        {
            _db.Open();
            if (cancellationToken.IsCancellationRequested)
                throw new TaskCanceledException("Cancellation requested in GetFullInterventionByIdAsync");

            return _db.QuerySingleAsync<FullInterventionDto?>(sql, new { id });
        }
        finally
        {
            _db.Close();
        }
    }

    public async Task AddInterventionAsync(Intervention intervention, CancellationToken cancellationToken)
    {
        // Start a transaction to ensure all inserts are successful
        _db.Open();
        using var transaction = _db.BeginTransaction();
        try
        {
            // Insert customer if it does not exist
            var fullCustomerName = $"{intervention.Site.Customer.Name} {intervention.Site.Customer.Surname}";

            // Check if customer exists
            var sql = "SELECT id FROM Customers WHERE CONCAT(name, ' ', surname) = @FullCustomerName;";
            var customerId =
                await _db.QuerySingleOrDefaultAsync<int>(sql, new { FullCustomerName = fullCustomerName }, transaction);

            if (customerId == 0)
            {
                // Insert new customer
                sql =
                    "INSERT INTO Customers (name, surname, vat, email, stored, creation_date, last_update_date) VALUES (@Name, @Surname, @Vat, @Email, FALSE, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP) RETURNING id;";
                customerId = await _db.QuerySingleAsync<int>(sql, intervention.Site.Customer, transaction);
            }

            // Insert site if it does not exist
            sql =
                "INSERT INTO Sites (name, address, customer_id, stored, creation_date, last_update_date) VALUES (@Name, @Address, @CustomerId, FALSE, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP) ON CONFLICT (name) DO NOTHING RETURNING id;";
            var siteId = await _db.QuerySingleOrDefaultAsync<int>(sql,
                new { Name = intervention.Site.Name, Address = intervention.Site.Address, CustomerId = customerId },
                transaction);

            if (siteId == 0)
            {
                sql = "SELECT id FROM Sites WHERE name = @Name;";
                siteId = await _db.QuerySingleAsync<int>(sql, new { Name = intervention.Site.Name }, transaction);
            }

            sql =
                "INSERT INTO Interventions (site_id, title, description, intervention_date, stored, creation_date, last_update_date) VALUES (@SiteId, @Title, @Description, @InterventionDate, FALSE, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP) RETURNING id;";
            var interventionId = await _db.QuerySingleAsync<int>(sql,
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
                await _db.ExecuteAsync(sql, material.Material, transaction);

                sql =
                    "INSERT INTO InterventionMaterials (intervention_id, material_id, quantity, stored, creation_date, last_update_date) VALUES (@InterventionId, @MaterialId, @Quantity, FALSE, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);";
                await _db.ExecuteAsync(sql,
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
                await _db.ExecuteAsync(sql, workerHour.Worker, transaction);

                sql =
                    "INSERT INTO WorkerInterventions (worker_id, intervention_id, hours_worked, stored, creation_date, last_update_date) VALUES (@WorkerId, @InterventionId, @HoursWorked, FALSE, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);";
                await _db.ExecuteAsync(sql,
                    new
                    {
                        WorkerId = workerHour.Worker.Id, InterventionId = interventionId,
                        HoursWorked = workerHour.Hours
                    }, transaction);
            }

            transaction.Commit();
        }
        catch (Exception)
        {
            transaction.Rollback();
            throw; // rethrowing the exception to be handled by upper layers
        }
        finally
        {
            _db.Close();
        }
    }

    public void DeleteIntervention(int id)
    {
        IDbTransaction? transaction = null;
        try
        {
            _db.Open();
            transaction = _db.BeginTransaction();

            var sql = "UPDATE Interventions SET stored = TRUE WHERE id = @id";
            _db.Execute(sql, new { id }, transaction);

            sql = "UPDATE InterventionMaterials SET stored = TRUE WHERE intervention_id = @id";
            _db.Execute(sql, transaction);

            sql = "UPDATE WorkerInterventions SET stored = TRUE WHERE intervention_id = @id";
            _db.Execute(sql, transaction);

            transaction.Commit();
        }
        catch (Exception)
        {
            transaction?.Rollback();
            throw;
        }
        finally
        {
            _db.Close();
        }
    }
}