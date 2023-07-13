using SimtekMaui.Data.Models;

namespace SimtekMaui.Application
{
    public class FakeCustomerRepository : ICustomerRepository
    {
        public Task<List<CustomerDto>> GetAsync(CancellationToken cancellationToken = default, bool getStored = false)
        {
            throw new NotImplementedException();
        }
    }
}