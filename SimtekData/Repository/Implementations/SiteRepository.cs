using System.Data;
using Dapper;
using Npgsql;
using SimtekData.Models;
using SimtekDomain;

namespace SimtekData;

public class SiteRepository
{
    private readonly IDbConnection _db;

    public SiteRepository(string connectionString)
    {
        _db = new NpgsqlConnection(connectionString);
    }

    public List<SiteDto> GetSites()
    {
        return _db.Query<SiteDto>("SELECT * FROM Sites WHERE stored = FALSE").ToList();
    }

    public SiteDto GetSite(int id)
    {
        return _db.QuerySingle<SiteDto>("SELECT * FROM Sites WHERE id = @id AND stored = FALSE", new { id });
    }

    public void AddSite(SiteDto siteDto)
    {
        var sql = "INSERT INTO Sites (name, address, customer_id, stored, creation_date, last_update_date) VALUES (@Name, @Address, @CustomerId, FALSE, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);";
        _db.Execute(sql, siteDto);
    }

    public void UpdateSite(SiteDto siteDto)
    {
        var sql = "UPDATE Sites SET name = @Name, address = @Address, customer_id = @CustomerId, last_update_date = CURRENT_TIMESTAMP WHERE id = @Id";
        _db.Execute(sql, siteDto);
    }

    public void DeleteSite(int id)
    {
        var sql = "UPDATE Sites SET stored = TRUE WHERE id = @id";
        _db.Execute(sql, new { id });
    }

    public List<SiteDto> GetSitesByIds(List<int> ids)
    {
        try
        {
            _db.Open();
            var result = _db.Query<SiteDto>("SELECT * from sites where id in(@Ids)", new { Ids = ids }).ToList();
            return result;
        }
        finally
        {
            _db.Close();
        }
        
            
    }
}
