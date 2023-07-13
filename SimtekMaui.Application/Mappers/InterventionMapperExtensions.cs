using SimtekMaui.Data.Models.Intervention;
using SimtekMaui.Models;

namespace SimtekMaui.Application.Mappers;

public static class InterventionMapperExtensions
{
    public static Intervention ToDomainModel(this InterventionDto dto)
    {
        return new Intervention(
           dto.Id,
           dto.Title,
           dto.InterventionDate,
           new Site(
               dto.SiteName,
               dto.SiteAddress,
               new Customer(dto.CustomerName, dto.CustomerSurname, dto.CustomerAddress)));
    }
}