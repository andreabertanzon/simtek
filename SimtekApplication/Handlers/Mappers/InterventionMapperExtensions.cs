using SimtekData.Models;
using SimtekData.Models.Intervention;
using SimtekData.Models.Worker;
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
            SiteName: dto.SiteDto.Name,
            Site: dto.SiteDto.ToDomainModel(dto.CustomerDto),
            Title:dto.InterventionShortDto.Title,
            Description: dto.InterventionShortDto.Description,
            WorkerHours: dto.WorkerDto.Select(x=>
                new WorkerHour(
                    Worker: new Worker(x.Id,x.Name,x.Surname,x.Pph), 
                    Hours: x.HourWorked))
                .ToList(),
            Materials: dto.MaterialDto.Select(m=>new MaterialUse(
                Material: m.ToDomainModel(),
                Quantity: m.Quantity)).ToList(),
            dto.InterventionShortDto.InterventionDate,
            dto.InterventionShortDto.Stored) ;
    }
}