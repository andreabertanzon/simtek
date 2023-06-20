using System.Data;
using Dapper;
using Npgsql;
using SimtekData.Models;

namespace SimtekData.Repository;

public class CustomerRepository
{
    private readonly IDbConnection _db;

    public CustomerRepository(string connectionString)
    {
        _db = new NpgsqlConnection(connectionString);
    }

    public List<CustomerDto> GetCustomersByIds(List<int> ids)
    {
        try
        {
            _db.Open();
            var customerDtos = _db.Query<CustomerDto>("SELECT * FROM customers WHERE id in(@Ids)", new{Ids = ids}).ToList();
            return customerDtos;
        }
        finally
        {
            _db.Close();
        }
    }
}