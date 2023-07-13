using SimtekMaui.Data.Models;
using SimtekMaui.Models;

namespace SimtekMaui.Application.Mappers;

public static class CustomerMapperExtensions
{
    public static Customer ToDomainModel(this CustomerDto dto)
    {
        return new Customer(dto.Name, dto.Surname, dto.Address, dto.Id, dto.Vat, dto.Email, dto.PhoneNumber);   
    }
}