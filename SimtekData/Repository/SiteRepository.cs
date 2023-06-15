using System.Data;
using Dapper;
using Npgsql;
using SimtekDomain;
using Site = SimtekData.Models.Site;

namespace SimtekData;

public class SiteRepository
{
    private readonly IDbConnection _db;

    public SiteRepository(string connectionString)
    {
        _db = new NpgsqlConnection(connectionString);
    }

    public List<Site> GetSites()
    {
        return _db.Query<Site>("SELECT * FROM Sites WHERE stored = FALSE").ToList();
    }

    public Site GetSite(int id)
    {
        return _db.QuerySingle<Site>("SELECT * FROM Sites WHERE id = @id AND stored = FALSE", new { id });
    }

    public void AddSite(Site site)
    {
        var sql = "INSERT INTO Sites (name, address, customer_id, stored, creation_date, last_update_date) VALUES (@Name, @Address, @CustomerId, FALSE, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);";
        _db.Execute(sql, site);
    }

    public void UpdateSite(Site site)
    {
        var sql = "UPDATE Sites SET name = @Name, address = @Address, customer_id = @CustomerId, last_update_date = CURRENT_TIMESTAMP WHERE id = @Id";
        _db.Execute(sql, site);
    }

    public void DeleteSite(int id)
    {
        var sql = "UPDATE Sites SET stored = TRUE WHERE id = @id";
        _db.Execute(sql, new { id });
    }
}
