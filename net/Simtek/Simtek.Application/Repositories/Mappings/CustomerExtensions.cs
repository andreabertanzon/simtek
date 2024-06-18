using Simtek.Data;

namespace Simtek.Application.Repositories.Mappings;

public static class CustomerExtensions
{
    public static IEnumerable<Domain.Customer> ToDomain(this IEnumerable<Data.Customer> entity)
    {
        return entity.Select(x => new Domain.Customer
        {
            Id = x.Id,
            Name = x.Name,
            Surname = x.Surname,
            Address = x.Address,
            City = x.City,
            Zip = x.Zip,
            Vat = x.Vat,
            Phone = x.Phone,
            Email = x.Email,
        });
    }
    
    public static Domain.Customer ToDomain(this Data.Customer entity)
    {
        return new Domain.Customer
        {
            Id = entity.Id,
            Name = entity.Name,
            Surname = entity.Surname,
            Address = entity.Address,
            City = entity.City,
            Zip = entity.Zip,
            Vat = entity.Vat,
            Phone = entity.Phone,
            Email = entity.Email,
            Sites = entity.Sites.ToDomain().ToList(),
        };
    }
    
    public static Data.Customer ToData(this Domain.Customer entity)
    {
        return new Data.Customer
        {
            Id = entity.Id,
            Name = entity.Name,
            Surname = entity.Surname,
            Address = entity.Address,
            City = entity.City,
            Zip = entity.Zip,
            Vat = entity.Vat,
            Phone = entity.Phone,
            Email = entity.Email ?? "mail@mancalemail.com",
        };
    }
}