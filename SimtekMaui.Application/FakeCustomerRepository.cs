using SimtekMaui.Data.Models;

namespace SimtekMaui.Application
{
    public class FakeCustomerRepository : ICustomerRepository
    {
        public async Task<List<CustomerDto>> GetAsync(CancellationToken cancellationToken = default, bool getStored = false)
        {
            return Context.Customers.ToList();
        } 
    }
}