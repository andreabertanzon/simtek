using System.Data;
using Dapper;
using Npgsql;
using SimtekData.Models;
using SimtekDomain;

namespace SimtekData.Repository;

public class InterventionRepository
{
    private readonly IDbConnection _db;

    public InterventionRepository(string connectionString)
    {
        _db = new NpgsqlConnection(connectionString);
    }

    public List<InterventionDto> GetInterventions()
    {
        return _db.Query<InterventionDto>("SELECT * FROM Interventions WHERE stored = FALSE").ToList();
    }

    public InterventionDto GetIntervention(int id)
    {
        return _db.QuerySingle<InterventionDto>("SELECT * FROM Interventions WHERE id = @id AND stored = FALSE", new { id });
    }

    public void AddIntervention(Intervention intervention)
    {
        // Start a transaction to ensure all inserts are successful
        using var transaction = _db.BeginTransaction();
        try
        {
            var sql = "INSERT INTO Interventions (site_id, intervention_date, stored, creation_date, last_update_date) VALUES (@SiteId, @InterventionDate, FALSE, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP) RETURNING id;";
            var interventionId = _db.QuerySingle<int>(sql, intervention, transaction);

            // Insert into InterventionMaterials
            foreach (var (material, quantity) in intervention.Materials)
            {
                // Insert material if it does not exist
                sql = "INSERT INTO Materials (id, name, price, unit, quantity, stored, creation_date, last_update_date) VALUES (@Id, @Name, @Price, @Unit, @Quantity, FALSE, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP) ON CONFLICT (id) DO NOTHING;";
                _db.Execute(sql, material, transaction);

                sql = "INSERT INTO InterventionMaterials (intervention_id, material_id, quantity, stored, creation_date, last_update_date) VALUES (@InterventionId, @MaterialId, @Quantity, FALSE, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);";
                _db.Execute(sql, new { InterventionId = interventionId, MaterialId = material.Id, Quantity = quantity }, transaction);
            }

            // Insert into WorkerInterventions
            foreach (var workerHour in intervention.WorkerHours)
            {
                // Insert worker if it does not exist
                sql = "INSERT INTO Workers (id, name, surname, pph, stored, creation_date, last_update_date) VALUES (@Id, @Name, @Surname, @Pph, FALSE, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP) ON CONFLICT (id) DO NOTHING;";
                _db.Execute(sql, workerHour.Key, transaction);

                sql = "INSERT INTO WorkerInterventions (worker_id, intervention_id, hours_worked, stored, creation_date, last_update_date) VALUES (@WorkerId, @InterventionId, @HoursWorked, FALSE, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);";
                _db.Execute(sql, new { WorkerId = workerHour.Key.Id, InterventionId = interventionId, HoursWorked = workerHour.Value }, transaction);
            }

            transaction.Commit();
        }
        catch (Exception)
        {
            transaction.Rollback();
            throw;  // rethrowing the exception to be handled by upper layers
        }
    }


    public void UpdateIntervention(InterventionDto interventionDto)
    {
        var sql = "UPDATE Interventions SET site_id = @SiteId, intervention_date = @InterventionDate, last_update_date = CURRENT_TIMESTAMP WHERE id = @Id";
        _db.Execute(sql, interventionDto);
    }

    public void DeleteIntervention(int id)
    {
        var sql = "UPDATE Interventions SET stored = TRUE WHERE id = @id";
        _db.Execute(sql, new { id });
    }
}
