
using SimtekMaui.Data.Models;

public interface ICustomerRepository
{
     Task<List<CustomerDto>> GetAsync(CancellationToken cancellationToken = default, bool getStored = false);
}