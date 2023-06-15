using System.Data;
using Dapper;
using Npgsql;
using SimtekData.Models;
using SimtekDomain;

namespace SimtekData.Repository;

public class InterventionRepository
{
    private readonly IDbConnection _db;

    public InterventionRepository(IDbConnection db)
    {
        _db = db;
    }

    public List<InterventionDto> GetInterventions()
    {
        return _db.Query<InterventionDto>("SELECT * FROM Interventions WHERE stored = FALSE").ToList();
    }

    public InterventionDto GetIntervention(int id)
    {
        return _db.QuerySingle<InterventionDto>("SELECT * FROM Interventions WHERE id = @id AND stored = FALSE",
            new { id });
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
            var customerId = await _db.QuerySingleOrDefaultAsync<int>(sql, new { FullCustomerName = fullCustomerName }, transaction);

            if (customerId == 0)
            {
                // Insert new customer
                sql = "INSERT INTO Customers (name, surname, vat, email, stored, creation_date, last_update_date) VALUES (@Name, @Surname, @Vat, @Email, FALSE, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP) RETURNING id;";
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
                "INSERT INTO Interventions (site_id, intervention_date, stored, creation_date, last_update_date) VALUES (@SiteId, @InterventionDate, FALSE, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP) RETURNING id;";
            var interventionId = await _db.QuerySingleAsync<int>(sql,
                new { SiteId = siteId, InterventionDate = intervention.InterventionDate }, transaction);

            // Insert into InterventionMaterials
            foreach (var material in intervention.Materials)
            {
                // Insert material if it does not exist
                sql =
                    "INSERT INTO Materials (id, name, price, unit, quantity, stored, creation_date, last_update_date) VALUES (@Id, @Name, @Price, @Unit, @Quantity, FALSE, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP) ON CONFLICT (id) DO NOTHING;";
                await _db.ExecuteAsync(sql, material.Key, transaction);

                sql =
                    "INSERT INTO InterventionMaterials (intervention_id, material_id, quantity, stored, creation_date, last_update_date) VALUES (@InterventionId, @MaterialId, @Quantity, FALSE, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);";
                await _db.ExecuteAsync(sql,
                    new
                    {
                        InterventionId = interventionId, MaterialId = material.Key.Id, Quantity = material.Value
                    }, transaction);
            }

            // Insert into WorkerInterventions
            foreach (var workerHour in intervention.WorkerHours)
            {
                // Insert worker if it does not exist
                sql =
                    "INSERT INTO Workers (id, name, surname, pph, stored, creation_date, last_update_date) VALUES (@Id, @Name, @Surname, @Pph, FALSE, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP) ON CONFLICT (id) DO NOTHING;";
                await _db.ExecuteAsync(sql, workerHour.Key, transaction);

                sql =
                    "INSERT INTO WorkerInterventions (worker_id, intervention_id, hours_worked, stored, creation_date, last_update_date) VALUES (@WorkerId, @InterventionId, @HoursWorked, FALSE, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);";
                await _db.ExecuteAsync(sql,
                    new
                    {
                        WorkerId = workerHour.Key.Id, InterventionId = interventionId,
                        HoursWorked = workerHour.Value
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

    public void UpdateIntervention(InterventionDto interventionDto)
    {
        var sql =
            "UPDATE Interventions SET site_id = @SiteId, intervention_date = @InterventionDate, last_update_date = CURRENT_TIMESTAMP WHERE id = @Id";
        _db.Execute(sql, interventionDto);
    }

    public void DeleteIntervention(int id)
    {
        var sql = "UPDATE Interventions SET stored = TRUE WHERE id = @id";
        _db.Execute(sql, new { id });
    }
}