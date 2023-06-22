using MediatR;
using OneOf;
using SimtekApplication.Handlers.Mappers;
using SimtekData.Repository;
using SimtekData.Repository.Abstractions;
using SimtekDomain.Errors;
using SimtekDomain.InterventionCQRS;

namespace SimtekApplication.Handlers.Intervention;

public class
    GetInterventionByIdQueryHandler : IRequestHandler<GetInterventionByIdQuery,
        OneOf<SimtekDomain.InterventionShort, SimtekError>>
{
    private readonly IInterventionRepository _interventionRepository;

    public GetInterventionByIdQueryHandler(IInterventionRepository interventionRepository)
    {
        _interventionRepository = interventionRepository;
    }

    public async Task<OneOf<SimtekDomain.InterventionShort, SimtekError>> Handle(GetInterventionByIdQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            var interventionDto =
                await _interventionRepository.GetInterventionByIdAsync(request.Id, cancellationToken: cancellationToken);

            return interventionDto is null
                ? new SimtekError(new NotFoundError())
                : interventionDto.ToDomainModel();
        }
        catch (Exception ex)
        {
            return new SimtekError(new DatabaseError(ex));
        }
    }
}