using System.Data;
using Dapper;
using Npgsql;
using SimtekDomain;
using Material = SimtekData.Models.Material;

namespace SimtekData;

public class MaterialRepository
{
    private readonly IDbConnection _db;

    public MaterialRepository(string connectionString)
    {
        _db = new NpgsqlConnection(connectionString);
    }

    public List<Material> GetMaterials()
    {
        return _db.Query<Material>("SELECT * FROM Materials WHERE stored = FALSE").ToList();
    }

    public Material GetMaterial(int id)
    {
        return _db.QuerySingle<Material>("SELECT * FROM Materials WHERE id = @id AND stored = FALSE", new { id });
    }

    public void AddMaterial(Material material)
    {
        var sql = "INSERT INTO Materials (name, price, unit, quantity, stored, creation_date, last_update_date) VALUES (@Name, @Price, @Unit, @Quantity, FALSE, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);";
        _db.Execute(sql, material);
    }

    public void UpdateMaterial(Material material)
    {
        var sql = "UPDATE Materials SET name = @Name, price = @Price, unit = @Unit, quantity = @Quantity, last_update_date = CURRENT_TIMESTAMP WHERE id = @Id";
        _db.Execute(sql, material);
    }

    public void DeleteMaterial(int id)
    {
        var sql = "UPDATE Materials SET stored = TRUE WHERE id = @id";
        _db.Execute(sql, new { id });
    }
}
