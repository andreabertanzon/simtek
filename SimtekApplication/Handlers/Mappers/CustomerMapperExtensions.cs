using SimtekData.Models;
using SimtekDomain;

namespace SimtekApplication.Handlers.Mappers;

public static class CustomerMapperExtensions
{
    public static SimtekDomain.Customer ToDomainModel(this CustomerDto dto)
    {
        return new Customer(
            Id: dto.Id,
            Name: dto.Name,
            Surname: dto.Surname,
            Address: "",
            Vat: dto.Vat,
            Email: dto.Email,
            PhoneNumber: "");
    }
}