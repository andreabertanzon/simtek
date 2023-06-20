using SimtekData.Models;
using SimtekData.Models.Intervention;
using SimtekDomain;

namespace SimtekApplication.Handlers.Mappers;

public static class InterventionMapperExtensions
{
    public static SimtekDomain.Intervention ToDomainModel(this FullInterventionDto dto)
    {
        return new SimtekDomain.Intervention(
            dto.Id,
            new Site(dto.SiteId, dto.SiteName, dto.SiteAddress,
                new Customer(
                    dto.CustomerId,
                    dto.CustomerName,
                    dto.CustomerSurname,
                    dto.CustomerAddress,
                    dto.CustomerVat,
                    dto.CustomerEmail,
                    dto.CustomerPhoneNumber)),
            dto.Title,
            dto.Description,
            new List<WorkerHour>
            {
                new WorkerHour(new Worker(dto.WorkerId, dto.WorkerName, dto.WorkerSurname, dto.WorkerPph),
                    Hours: dto.HoursWorked)
            },
            new List<MaterialUse>
            {
                new MaterialUse(
                    new SimtekDomain.Material(dto.MaterialId, dto.MaterialName, dto.MaterialPrice, dto.MaterialUnit,
                        dto.MaterialQuantity),
                    dto.MaterialQuantity)
            },
            dto.InterventionDate,
            dto.Stored);
    }
}