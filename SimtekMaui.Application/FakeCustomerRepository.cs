using SimtekMaui.Data.Models;

namespace SimtekMaui.Application
{
    public class FakeCustomerRepository : ICustomerRepository
    {
        public async Task<List<CustomerDto>> GetAsync(CancellationToken cancellationToken = default, bool getStored = false)
        {
            return new List<CustomerDto>()
            {
                new CustomerDto()
                {
                    Id = Guid.NewGuid(),
                    Name = "Angelina",
                    Surname ="Pippoletta",
                    Vat ="1234",
                    Address ="Via dei prati, Pescantina, VR",
                    PhoneNumber ="123456",
                    Email ="angelina@pippolina.com",
                    Stored =false,
                    CreationDate =DateTime.Now,
                    LastUpdateDate =DateTime.Now,
                },
                new CustomerDto()
                {
                    Id = Guid.NewGuid(),
                    Name = "Angelina",
                    Surname ="Pippoletta",
                    Vat ="1234",
                    Address ="Via dei prati, Pescantina, VR",
                    PhoneNumber ="123456",
                    Email ="angelina@pippolina.com",
                    Stored =false,
                    CreationDate =DateTime.Now,
                    LastUpdateDate =DateTime.Now,
                },
                new CustomerDto()
                {
                    Id = Guid.NewGuid(),
                    Name = "Angelina",
                    Surname ="Pippoletta",
                    Vat ="1234",
                    Address ="Via dei prati, Pescantina, VR",
                    PhoneNumber ="123456",
                    Email ="angelina@pippolina.com",
                    Stored =false,
                    CreationDate =DateTime.Now,
                    LastUpdateDate =DateTime.Now,
                },
                new CustomerDto()
                {
                    Id = Guid.NewGuid(),
                    Name = "Angelina",
                    Surname ="Pippoletta",
                    Vat ="1234",
                    Address ="Via dei prati, Pescantina, VR",
                    PhoneNumber ="123456",
                    Email ="angelina@pippolina.com",
                    Stored =false,
                    CreationDate =DateTime.Now,
                    LastUpdateDate =DateTime.Now,
                },
                new CustomerDto()
                {
                    Id = Guid.NewGuid(),
                    Name = "Angelina",
                    Surname ="Pippoletta",
                    Vat ="1234",
                    Address ="Via dei prati, Pescantina, VR",
                    PhoneNumber ="123456",
                    Email ="angelina@pippolina.com",
                    Stored =false,
                    CreationDate =DateTime.Now,
                    LastUpdateDate =DateTime.Now,
                },
                new CustomerDto()
                {
                    Id = Guid.NewGuid(),
                    Name = "Angelina",
                    Surname ="Pippoletta",
                    Vat ="1234",
                    Address ="Via dei prati, Pescantina, VR",
                    PhoneNumber ="123456",
                    Email ="angelina@pippolina.com",
                    Stored =false,
                    CreationDate =DateTime.Now,
                    LastUpdateDate =DateTime.Now,
                },
                new CustomerDto()
                {
                    Id = Guid.NewGuid(),
                    Name = "Angelina",
                    Surname ="Pippoletta",
                    Vat ="1234",
                    Address ="Via dei prati, Pescantina, VR",
                    PhoneNumber ="123456",
                    Email ="angelina@pippolina.com",
                    Stored =false,
                    CreationDate =DateTime.Now,
                    LastUpdateDate =DateTime.Now,
                },
                new CustomerDto()
                {
                    Id = Guid.NewGuid(),
                    Name = "Angelina",
                    Surname ="Pippoletta",
                    Vat ="1234",
                    Address ="Via dei prati, Pescantina, VR",
                    PhoneNumber ="123456",
                    Email ="angelina@pippolina.com",
                    Stored =false,
                    CreationDate =DateTime.Now,
                    LastUpdateDate =DateTime.Now,
                },
                new CustomerDto()
                {
                    Id = Guid.NewGuid(),
                    Name = "Angelina",
                    Surname ="Pippoletta",
                    Vat ="1234",
                    Address ="Via dei prati, Pescantina, VR",
                    PhoneNumber ="123456",
                    Email ="angelina@pippolina.com",
                    Stored =false,
                    CreationDate =DateTime.Now,
                    LastUpdateDate =DateTime.Now,
                },
                new CustomerDto()
                {
                    Id = Guid.NewGuid(),
                    Name = "Angelina",
                    Surname ="Pippoletta",
                    Vat ="1234",
                    Address ="Via dei prati, Pescantina, VR",
                    PhoneNumber ="123456",
                    Email ="angelina@pippolina.com",
                    Stored =false,
                    CreationDate =DateTime.Now,
                    LastUpdateDate =DateTime.Now,
                },
                new CustomerDto()
                {
                    Id = Guid.NewGuid(),
                    Name = "Angelina",
                    Surname ="Pippoletta",
                    Vat ="1234",
                    Address ="Via dei prati, Pescantina, VR",
                    PhoneNumber ="123456",
                    Email ="angelina@pippolina.com",
                    Stored =false,
                    CreationDate =DateTime.Now,
                    LastUpdateDate =DateTime.Now,
                },
                new CustomerDto()
                {
                    Id = Guid.NewGuid(),
                    Name = "Angelina",
                    Surname ="Pippoletta",
                    Vat ="1234",
                    Address ="Via dei prati, Pescantina, VR",
                    PhoneNumber ="123456",
                    Email ="angelina@pippolina.com",
                    Stored =false,
                    CreationDate =DateTime.Now,
                    LastUpdateDate =DateTime.Now,
                },
                new CustomerDto()
                {
                    Id = Guid.NewGuid(),
                    Name = "Angelina",
                    Surname ="Pippoletta",
                    Vat ="1234",
                    Address ="Via dei prati, Pescantina, VR",
                    PhoneNumber ="123456",
                    Email ="angelina@pippolina.com",
                    Stored =false,
                    CreationDate =DateTime.Now,
                    LastUpdateDate =DateTime.Now,
                },
                new CustomerDto()
                {
                    Id = Guid.NewGuid(),
                    Name = "Angelina",
                    Surname ="Pippoletta",
                    Vat ="1234",
                    Address ="Via dei prati, Pescantina, VR",
                    PhoneNumber ="123456",
                    Email ="angelina@pippolina.com",
                    Stored =false,
                    CreationDate =DateTime.Now,
                    LastUpdateDate =DateTime.Now,
                },
                new CustomerDto()
                {
                    Id = Guid.NewGuid(),
                    Name = "Angelina",
                    Surname ="Pippoletta",
                    Vat ="1234",
                    Address ="Via dei prati, Pescantina, VR",
                    PhoneNumber ="123456",
                    Email ="angelina@pippolina.com",
                    Stored =false,
                    CreationDate =DateTime.Now,
                    LastUpdateDate =DateTime.Now,
                },
                new CustomerDto()
                {
                    Id = Guid.NewGuid(),
                    Name = "Angelina",
                    Surname ="Pippoletta",
                    Vat ="1234",
                    Address ="Via dei prati, Pescantina, VR",
                    PhoneNumber ="123456",
                    Email ="angelina@pippolina.com",
                    Stored =false,
                    CreationDate =DateTime.Now,
                    LastUpdateDate =DateTime.Now,
                },
                new CustomerDto()
                {
                    Id = Guid.NewGuid(),
                    Name = "Angelina",
                    Surname ="Pippoletta",
                    Vat ="1234",
                    Address ="Via dei prati, Pescantina, VR",
                    PhoneNumber ="123456",
                    Email ="angelina@pippolina.com",
                    Stored =false,
                    CreationDate =DateTime.Now,
                    LastUpdateDate =DateTime.Now,
                },
                new CustomerDto()
                {
                    Id = Guid.NewGuid(),
                    Name = "Angelina",
                    Surname ="Pippoletta",
                    Vat ="1234",
                    Address ="Via dei prati, Pescantina, VR",
                    PhoneNumber ="123456",
                    Email ="angelina@pippolina.com",
                    Stored =false,
                    CreationDate =DateTime.Now,
                    LastUpdateDate =DateTime.Now,
                },

            };
        }
    }
}