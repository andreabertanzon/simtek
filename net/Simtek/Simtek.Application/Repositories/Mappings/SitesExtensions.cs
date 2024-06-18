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
        });
    }
}