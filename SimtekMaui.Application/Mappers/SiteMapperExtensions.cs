using SimtekMaui.Data.Models;
using SimtekMaui.Models;

namespace SimtekMaui.Application.Mappers;

public static class SiteMapperExtensions
{
    public static Site ToDomainModel(this SiteDto dto, CustomerDto customerDto)
    {
        return new Site(name: dto.Name, address:dto.Address, customer:customerDto.ToDomainModel());
    }
}