using SimtekDomain;
using SimtekMaui.Data.Models.Intervention;

namespace SimtekMaui.Application.Mappers;

public static class InterventionMapperExtensions
{
    public static Intervention ToDomainModel(this InterventionDto dto)
    {
        return new SimtekDomain.Intervention(
            Id: dto.Id,
            SiteName: dto.SiteName,
            Site: null,
            InterventionDate: dto.InterventionDate,
            Title: dto.Title,
            Description: dto.Description,
            WorkerHours: new List<SimtekDomain.WorkerHour>(),
            Materials: new List<MaterialUse>());
    }
}