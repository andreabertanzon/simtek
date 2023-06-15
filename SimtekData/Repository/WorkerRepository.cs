using System.Data;
using Dapper;
using Npgsql;
using SimtekDomain;

namespace SimtekData;

public class WorkerRepository
{
    private readonly IDbConnection _db;

    public WorkerRepository(string connectionString)
    {
        _db = new NpgsqlConnection(connectionString);
    }

    public List<Worker> GetWorkers()
    {
        return _db.Query<Worker>("SELECT * FROM Workers WHERE stored = FALSE").ToList();
    }

    public Worker GetWorker(int id)
    {
        return _db.QuerySingle<Worker>("SELECT * FROM Workers WHERE id = @id AND stored = FALSE", new { id });
    }

    public void AddWorker(Worker worker)
    {
        var sql = "INSERT INTO Workers (name, surname, pph, stored, creation_date, last_update_date) VALUES (@Name, @Surname, @Pph, FALSE, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);";
        _db.Execute(sql, worker);
    }

    public void UpdateWorker(Worker worker)
    {
        var sql = "UPDATE Workers SET name = @Name, surname = @Surname, pph = @Pph, last_update_date = CURRENT_TIMESTAMP WHERE id = @Id";
        _db.Execute(sql, worker);
    }

    public void DeleteWorker(int id)
    {
        var sql = "UPDATE Workers SET stored = TRUE WHERE id = @id";
        _db.Execute(sql, new { id });
    }
}
