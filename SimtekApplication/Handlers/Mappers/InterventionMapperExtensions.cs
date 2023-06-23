using SimtekData.Models;
using SimtekData.Models.Intervention;
using SimtekDomain;

namespace SimtekApplication.Handlers.Mappers;

public static class InterventionMapperExtensions
{
    public static SimtekDomain.Intervention ToDomainModel(this InterventionShortDto dto)
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

    public static SimtekDomain.Intervention ToDomainModel(this FullInterventionDto dto)
    {
        return new SimtekDomain.Intervention(
            Id: dto.InterventionShortDto.Id,
            dto.SiteDto.ToDomainModel(),
            dto.InterventionShortDto.Title,
            dto.InterventionShortDto.Description,
            dto.MaterialDto.Select(m => m.ToDomainModel()).ToList(),
            dto.WorkerDto.Select(w => w.ToDomainModel()).ToList(),
            dto.InterventionShortDto.InterventionDate,
            dto.InterventionShortDto.Stored);
    }
}