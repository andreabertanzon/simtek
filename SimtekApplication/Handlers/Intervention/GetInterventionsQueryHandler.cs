using MediatR;
using Npgsql;
using OneOf;
using OneOf.Types;
using SimtekData;
using SimtekData.Repository;
using SimtekDomain.Query;
using SimtekDomain;
using SimtekDomain.Errors;

namespace SimtekApplication.Handlers.Intervention;

public class
    GetInterventionsQueryHandler : IRequestHandler<GetInterventions,
        OneOf<List<SimtekDomain.Intervention>, SimtekError>>
{
    private readonly InterventionRepository _interventionRepository;


    public GetInterventionsQueryHandler(InterventionRepository interventionRepository)
    {
        _interventionRepository = interventionRepository;
      
    }

    public async Task<OneOf<List<SimtekDomain.Intervention>, SimtekError>> Handle(GetInterventions request,
        CancellationToken cancellationToken)
    {
        try
        {
            var fullInterventionDtos = await _interventionRepository.GetInterventions(cancellationToken);

            return fullInterventionDtos
                .Select(dto => 
                    new SimtekDomain.Intervention(
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
                        new List<WorkerHour>
                        {
                            new WorkerHour(new Worker(dto.WorkerId, dto.WorkerName, dto.WorkerSurname, dto.WorkerPph), Hours: dto.HoursWorked)
                        }, 
                        new List<MaterialUse>
                        {
                            new MaterialUse(
                                new Material(dto.MaterialId, dto.MaterialName, dto.MaterialPrice, dto.MaterialUnit, dto.MaterialQuantity), 
                                dto.MaterialQuantity)
                        },
                        dto.InterventionDate, 
                        dto.Stored))
                .ToList();
        }
        catch (Exception ex)
        {
            return new SimtekError(new DatabaseError(ex));
        }
    }
}