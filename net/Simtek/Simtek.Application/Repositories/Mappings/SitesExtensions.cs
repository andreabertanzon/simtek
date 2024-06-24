using Simtek.Data;

namespace Simtek.Application.Repositories.Mappings;

public static class SitesExtensions
{
    public static IEnumerable<Domain.Site> ToDomain(this IEnumerable<Data.Site> entity)
    {
        return entity.Select(x => new Domain.Site
        {
            Id = x.Id,
            Name = x.Name,
            Address = x.Address,
            City = x.City,
            ZipCode = x.Zip,
            CustomerId = x.CustomerId,
        });
    }
    
    public static Domain.Site ToDomain(this Site entity, Customer? customer = null)
    {
        return new Domain.Site
        {
            Id = entity.Id,
            Name = entity.Name,
            Address = entity.Address,
            City = entity.City,
            ZipCode = entity.Zip,
            CustomerId = entity.CustomerId,
        };
    }
    
    public static Data.Site ToData(this Domain.Site entity)
    {
        return new Data.Site
        {
            Id = entity.Id,
            Name = entity.Name,
            Address = entity.Address,
            City = entity.City,
            Zip = entity.ZipCode,
            CustomerId = entity.CustomerId,
        };
    }
}