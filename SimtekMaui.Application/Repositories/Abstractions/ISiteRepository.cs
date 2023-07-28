using SimtekMaui.Data.Models;
using SimtekMaui.Models;

namespace SimtekMaui.Data.Repositories.Abstractions;

public interface ISiteRepository
{
    public Task<List<Site>> GetAllByCustomerAsync(Guid customerId, CancellationToken cancellationToken);
}