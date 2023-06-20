using SimtekData.Models;

namespace SimtekApplication.Handlers.Mappers;

public static class MaterialMapperExtensions
{
    public static SimtekDomain.Material ToDomainModel(this MaterialDto dto)
    {
        return new SimtekDomain.Material(
            dto.Id,
            dto.Name,
            dto.Price,
            dto.Unit,
            dto.Quantity);
    }
}