using Dapper;
using Npgsql;
using SimtekData.Configurations;
using SimtekData.Models;
using SimtekData.Repository.Abstractions;

namespace SimtekData.Repository.Implementations;

public class MaterialRepository : IMaterialRepository
{
    private readonly string _dbConnectionLiteral;

    public MaterialRepository(DbConnectionLiteral dbConnectionLiteral)
    {
        _dbConnectionLiteral = dbConnectionLiteral.ConnectionString;
    }

    public async Task<List<MaterialDto>> GetMaterialsAsync(CancellationToken cancellationToken)
    {
        await using var connection = new NpgsqlConnection(_dbConnectionLiteral);

        return (await connection.QueryAsync<MaterialDto>("SELECT * FROM Materials WHERE stored = FALSE",
            cancellationToken))
            .ToList();
    }

    public async Task<MaterialDto?> GetMaterialByIdAsync(int id, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        
        await using var connection = new NpgsqlConnection(_dbConnectionLiteral);
        return await connection.QuerySingleAsync<MaterialDto>(
            "SELECT * FROM Materials WHERE id = @id AND stored = FALSE",
            new { id });
    }

    public async Task AddMaterialAsync(MaterialDto materialDto, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        await using var connection = new NpgsqlConnection(_dbConnectionLiteral);
        
        var sql =
            "INSERT INTO Materials (name, price, unit, quantity, stored, creation_date, last_update_date) VALUES (@Name, @Price, @Unit, @Quantity, FALSE, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);";
        await connection.ExecuteAsync(sql, materialDto);
    }

    public async Task UpdateMaterialAsync(MaterialDto materialDto, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        await using var connection = new NpgsqlConnection(_dbConnectionLiteral);
        
        var sql =
            "UPDATE Materials SET name = @Name, price = @Price, unit = @Unit, quantity = @Quantity, last_update_date = CURRENT_TIMESTAMP WHERE id = @Id";
        await connection.ExecuteAsync(sql, materialDto);
    }

    public async Task StoreMaterialAsync(int id, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        await using var connection = new NpgsqlConnection(_dbConnectionLiteral);
        
        var sql = "UPDATE Materials SET stored = TRUE WHERE id = @id";
        await connection.ExecuteAsync(sql, new { id });
    }
}