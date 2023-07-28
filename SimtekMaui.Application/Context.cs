using SimtekMaui.Data.Models;
using SimtekMaui.Data.Models.Intervention;

namespace SimtekMaui.Application;

public static class Context
{
    public static List<InterventionDto> InterventionDtos { get; }= new()
    {
        new InterventionDto
        {
            Id = Guid.NewGuid(),
            SiteId = Guid.NewGuid(),
            SiteName = "Condominio Piccia",
            CustomerName = "Loretta Bianchini",
            Title = "Bagni secondo piano",
            Description = "Intervento di manutenzione dell'impianto di condizionamento condominiale",
            InterventionDate = default,
            Stored = false
        },
        new InterventionDto
        {
            Id = Guid.NewGuid(),
            SiteId = Guid.NewGuid(),
            SiteName = "Casa Pidocchi",
            CustomerName = "Luigi Pidocchi",
            Title = "Prese cucina",
            Description = "Intervento di manutenzione dell'impianto di condizionamento condominiale",
            InterventionDate = default,
            Stored = false
        },
        new InterventionDto
        {
            Id = Guid.NewGuid(),
            SiteId = Guid.NewGuid(),
            SiteName = "Condominio Loreto",
            CustomerName = "Piccioni",
            Title = "Bagni secondo piano",
            Description = "Intervento di manutenzione dell'impianto di condizionamento condominiale",
            InterventionDate = default,
            Stored = false
        },
        new InterventionDto
        {
            Id = Guid.NewGuid(),
            SiteId = Guid.NewGuid(),
            SiteName = "Condominio Donatini",
            CustomerName = "Maria Donatini",
            Title = "Bagni secondo piano",
            Description = "Intervento di manutenzione dell'impianto di condizionamento condominiale",
            InterventionDate = default,
            Stored = false
        },
        new InterventionDto
        {
            Id = Guid.NewGuid(),
            SiteId = Guid.NewGuid(),
            SiteName = "Condominio Agilulfo",
            CustomerName = "Rodolfo Rodolfi",
            Title = "Bagni secondo piano",
            Description = "Intervento di manutenzione dell'impianto di condizionamento condominiale",
            InterventionDate = default,
            Stored = false
        },
        new InterventionDto
        {
            Id = Guid.NewGuid(),
            SiteId = Guid.NewGuid(),
            SiteName = "Condominio Piccia",
            CustomerName = "Loretta Bianchini",
            Title = "Bagni secondo piano",
            Description = "Intervento di manutenzione dell'impianto di condizionamento condominiale",
            InterventionDate = default,
            Stored = false
        },
    };

    public static IEnumerable<CustomerDto> Customers { get; } = new List<CustomerDto>()
    {
        new CustomerDto()
        {
            Id = Guid.NewGuid(),
            Name = "Angelina",
            Surname = "Pippoletta",
            Vat = "1234",
            Address = "Via dei prati, Pescantina, VR",
            PhoneNumber = "123456",
            Email = "angelina@pippolina.com",
            Stored = false,
            CreationDate = DateTime.Now,
            LastUpdateDate = DateTime.Now,
        },
        new CustomerDto()
        {
            Id = Guid.NewGuid(),
            Name = "Angelina",
            Surname = "Pippoletta",
            Vat = "1234",
            Address = "Via dei prati, Pescantina, VR",
            PhoneNumber = "123456",
            Email = "angelina@pippolina.com",
            Stored = false,
            CreationDate = DateTime.Now,
            LastUpdateDate = DateTime.Now,
        },
        new CustomerDto()
        {
            Id = Guid.NewGuid(),
            Name = "Angelina",
            Surname = "Pippoletta",
            Vat = "1234",
            Address = "Via dei prati, Pescantina, VR",
            PhoneNumber = "123456",
            Email = "angelina@pippolina.com",
            Stored = false,
            CreationDate = DateTime.Now,
            LastUpdateDate = DateTime.Now,
        },
        new CustomerDto()
        {
            Id = Guid.NewGuid(),
            Name = "Angelina",
            Surname = "Pippoletta",
            Vat = "1234",
            Address = "Via dei prati, Pescantina, VR",
            PhoneNumber = "123456",
            Email = "angelina@pippolina.com",
            Stored = false,
            CreationDate = DateTime.Now,
            LastUpdateDate = DateTime.Now,
        },
        new CustomerDto()
        {
            Id = Guid.NewGuid(),
            Name = "Angelina",
            Surname = "Pippoletta",
            Vat = "1234",
            Address = "Via dei prati, Pescantina, VR",
            PhoneNumber = "123456",
            Email = "angelina@pippolina.com",
            Stored = false,
            CreationDate = DateTime.Now,
            LastUpdateDate = DateTime.Now,
        },
        new CustomerDto()
        {
            Id = Guid.NewGuid(),
            Name = "Angelina",
            Surname = "Pippoletta",
            Vat = "1234",
            Address = "Via dei prati, Pescantina, VR",
            PhoneNumber = "123456",
            Email = "angelina@pippolina.com",
            Stored = false,
            CreationDate = DateTime.Now,
            LastUpdateDate = DateTime.Now,
        },
        new CustomerDto()
        {
            Id = Guid.NewGuid(),
            Name = "Angelina",
            Surname = "Pippoletta",
            Vat = "1234",
            Address = "Via dei prati, Pescantina, VR",
            PhoneNumber = "123456",
            Email = "angelina@pippolina.com",
            Stored = false,
            CreationDate = DateTime.Now,
            LastUpdateDate = DateTime.Now,
        },
        new CustomerDto()
        {
            Id = Guid.NewGuid(),
            Name = "Angelina",
            Surname = "Pippoletta",
            Vat = "1234",
            Address = "Via dei prati, Pescantina, VR",
            PhoneNumber = "123456",
            Email = "angelina@pippolina.com",
            Stored = false,
            CreationDate = DateTime.Now,
            LastUpdateDate = DateTime.Now,
        },
        new CustomerDto()
        {
            Id = Guid.NewGuid(),
            Name = "Angelina",
            Surname = "Pippoletta",
            Vat = "1234",
            Address = "Via dei prati, Pescantina, VR",
            PhoneNumber = "123456",
            Email = "angelina@pippolina.com",
            Stored = false,
            CreationDate = DateTime.Now,
            LastUpdateDate = DateTime.Now,
        },
        new CustomerDto()
        {
            Id = Guid.NewGuid(),
            Name = "Angelina",
            Surname = "Pippoletta",
            Vat = "1234",
            Address = "Via dei prati, Pescantina, VR",
            PhoneNumber = "123456",
            Email = "angelina@pippolina.com",
            Stored = false,
            CreationDate = DateTime.Now,
            LastUpdateDate = DateTime.Now,
        },
        new CustomerDto()
        {
            Id = Guid.NewGuid(),
            Name = "Angelina",
            Surname = "Pippoletta",
            Vat = "1234",
            Address = "Via dei prati, Pescantina, VR",
            PhoneNumber = "123456",
            Email = "angelina@pippolina.com",
            Stored = false,
            CreationDate = DateTime.Now,
            LastUpdateDate = DateTime.Now,
        },
        new CustomerDto()
        {
            Id = Guid.NewGuid(),
            Name = "Angelina",
            Surname = "Pippoletta",
            Vat = "1234",
            Address = "Via dei prati, Pescantina, VR",
            PhoneNumber = "123456",
            Email = "angelina@pippolina.com",
            Stored = false,
            CreationDate = DateTime.Now,
            LastUpdateDate = DateTime.Now,
        },
        new CustomerDto()
        {
            Id = Guid.NewGuid(),
            Name = "Angelina",
            Surname = "Pippoletta",
            Vat = "1234",
            Address = "Via dei prati, Pescantina, VR",
            PhoneNumber = "123456",
            Email = "angelina@pippolina.com",
            Stored = false,
            CreationDate = DateTime.Now,
            LastUpdateDate = DateTime.Now,
        },
        new CustomerDto()
        {
            Id = Guid.NewGuid(),
            Name = "Angelina",
            Surname = "Pippoletta",
            Vat = "1234",
            Address = "Via dei prati, Pescantina, VR",
            PhoneNumber = "123456",
            Email = "angelina@pippolina.com",
            Stored = false,
            CreationDate = DateTime.Now,
            LastUpdateDate = DateTime.Now,
        },
        new CustomerDto()
        {
            Id = Guid.NewGuid(),
            Name = "Angelina",
            Surname = "Pippoletta",
            Vat = "1234",
            Address = "Via dei prati, Pescantina, VR",
            PhoneNumber = "123456",
            Email = "angelina@pippolina.com",
            Stored = false,
            CreationDate = DateTime.Now,
            LastUpdateDate = DateTime.Now,
        },
        new CustomerDto()
        {
            Id = Guid.NewGuid(),
            Name = "Angelina",
            Surname = "Pippoletta",
            Vat = "1234",
            Address = "Via dei prati, Pescantina, VR",
            PhoneNumber = "123456",
            Email = "angelina@pippolina.com",
            Stored = false,
            CreationDate = DateTime.Now,
            LastUpdateDate = DateTime.Now,
        },
        new CustomerDto()
        {
            Id = Guid.NewGuid(),
            Name = "Angelina",
            Surname = "Pippoletta",
            Vat = "1234",
            Address = "Via dei prati, Pescantina, VR",
            PhoneNumber = "123456",
            Email = "angelina@pippolina.com",
            Stored = false,
            CreationDate = DateTime.Now,
            LastUpdateDate = DateTime.Now,
        },
        new CustomerDto()
        {
            Id = Guid.NewGuid(),
            Name = "Angelina",
            Surname = "Pippoletta",
            Vat = "1234",
            Address = "Via dei prati, Pescantina, VR",
            PhoneNumber = "123456",
            Email = "angelina@pippolina.com",
            Stored = false,
            CreationDate = DateTime.Now,
            LastUpdateDate = DateTime.Now,
        },

    };

    public static IEnumerable<SiteDto> Sites { get; } = new List<SiteDto>()
    {
        new()
        {
            Address = "Via Pallina 33",
            Id = Guid.NewGuid(),
            CustomerId = Customers.First().Id,
            CreationDate = DateTime.Now,
            LastUpdateDate = DateTime.Now,
            Name = "Condominio Rossini",
            Stored = false
        }
    };
}