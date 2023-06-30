using SimtekDomain;
using SimtekMaui.Data.Models;

namespace SimtekMaui.Application.Mappers;

public static class CustomerMapperExtensions
{
    public static Customer ToDomainModel(this CustomerDto dto)
    {
        return new Customer(
            Id: dto.Id,
            Name: dto.Name,
            Surname: dto.Surname,
            Address: dto.Address,
            Vat: dto.Vat,
            Email: dto.Email,
            PhoneNumber: dto.PhoneNumber);
    }
}