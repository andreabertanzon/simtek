
using SimtekMaui.Data.Models;

namespace SimtekData.Repository.Abstractions;

public interface ICustomerRepository
{
     Task<List<CustomerDto>> GetCustomersAsync(CancellationToken cancellationToken = default, bool getStored = false);

}