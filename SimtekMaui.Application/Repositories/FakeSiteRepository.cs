using SimtekMaui.Application.Mappers;
using SimtekMaui.Data.Models;
using SimtekMaui.Data.Repositories.Abstractions;
using SimtekMaui.Models;

namespace SimtekMaui.Application.Repositories;

public class FakeSiteRepository:ISiteRepository
{
    
    public async Task<List<Site>> GetAllByCustomerAsync(Guid customerId, CancellationToken cancellationToken)
    {
        var customer = Context.Customers.First(x => x.Id == customerId);
        var sites =  Context.Sites.Where(x=>x.CustomerId == customerId).ToList();

        var result = sites.Select(x => x.ToDomainModel(customer));
        return result.ToList();
    }
}