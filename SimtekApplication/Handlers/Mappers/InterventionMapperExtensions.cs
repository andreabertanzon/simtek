using SimtekData.Models;
using SimtekData.Models.Intervention;
using SimtekDomain;

namespace SimtekApplication.Handlers.Mappers;

public static class InterventionMapperExtensions
{
    public static SimtekDomain.InterventionShort ToDomainModel(this InterventionShortDto dto)
    {
        return new SimtekDomain.InterventionShort(
            Id:dto.Id,
            SiteName:dto.SiteName,
            InterventionDate:dto.InterventionDate,
            Title:dto.Title,
            Description:dto.Description,
            HourSpent:dto.HourSpent,
            TotalWorkerCost:dto.TotalWorkerCost,
            TotalMaterialCost:dto.TotalMaterialCost ?? 0);
    }
}