using SimtekData.Repository.Abstractions;
using SimtekMaui.Data.Models;
using SimtekMaui.Data.Models.Intervention;
using SimtekMaui.Data.Repositories.Abstractions;

namespace SimtekMaui.Data;

public class FakeCustomerRepository:ICustomerRepository
{

    public Task<List<CustomerDto>> GetAsync(CancellationToken cancellationToken = default, bool getStored = false)
    {
        var result = new List<CustomerDto>()
        {
            new CustomerDto
            {
                Id = 0,
                Name = "Andrea",
                Surname = "Bertanzon",
                Vat = "BRTNDR89B26L781T",
                Address = "Somewhere in the world",
                PhoneNumber = "12345678790",
                Email = "something@email.com",
                Stored = false,
                CreationDate = default,
                LastUpdateDate = default
            }
        };

        return Task.FromResult(result);
    }
}