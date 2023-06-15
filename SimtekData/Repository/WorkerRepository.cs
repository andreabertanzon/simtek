using System.Data;
using Dapper;
using Npgsql;
using SimtekData.Models;

namespace SimtekData.Repository;

public class WorkerRepository
{
    private readonly IDbConnection _db;

    public WorkerRepository(string connectionString)
    {
        _db = new NpgsqlConnection(connectionString);
    }

    public List<WorkerDto> GetWorkers()
    {
        return _db.Query<WorkerDto>("SELECT * FROM Workers WHERE stored = FALSE").ToList();
    }

    public WorkerDto GetWorker(int id)
    {
        return _db.QuerySingle<WorkerDto>("SELECT * FROM Workers WHERE id = @id AND stored = FALSE", new { id });
    }

    public void AddWorker(WorkerDto workerDto)
    {
        var sql = "INSERT INTO Workers (name, surname, pph, stored, creation_date, last_update_date) VALUES (@Name, @Surname, @Pph, FALSE, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);";
        _db.Execute(sql, workerDto);
    }

    public void UpdateWorker(WorkerDto workerDto)
    {
        var sql = "UPDATE Workers SET name = @Name, surname = @Surname, pph = @Pph, last_update_date = CURRENT_TIMESTAMP WHERE id = @Id";
        _db.Execute(sql, workerDto);
    }

    public void DeleteWorker(int id)
    {
        var sql = "UPDATE Workers SET stored = TRUE WHERE id = @id";
        _db.Execute(sql, new { id });
    }
}
