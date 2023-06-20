using MediatR;
using OneOf;
using SimtekData.Repository;
using SimtekData.Repository.Abstractions;
using SimtekDomain;
using SimtekDomain.Errors;
using SimtekDomain.InterventionCQRS;

namespace SimtekApplication.Handlers.Intervention;

public class
    GetShortInterventionsQueryHandler : IRequestHandler<GetShortInterventionsQuery,
        OneOf<List<InterventionShort>, SimtekError>>
{
    private readonly IInterventionRepository _interventionRepository;

    public GetShortInterventionsQueryHandler(IInterventionRepository interventionRepository)
    {
        _interventionRepository = interventionRepository;
    }

    public async Task<OneOf<List<InterventionShort>, SimtekError>> Handle(GetShortInterventionsQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            var interventions = _interventionRepository.GetShortInterventions();

            var result = interventions.Select(x => new InterventionShort(
                x.Id, x.SiteName, DateTime.Now, x.Title, x.Description, x.HourSpent, x.TotalWorkerCost,
                x.TotalMaterialCost ?? 0.0));

            return result.ToList();
        }
        catch (Exception e)
        {
            return new SimtekError(error: new DatabaseError(e));
        }
    }
}