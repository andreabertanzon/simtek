using MediatR;
using Npgsql;
using OneOf;
using OneOf.Types;
using SimtekApplication.Handlers.Mappers;
using SimtekData;
using SimtekData.Repository;
using SimtekData.Repository.Abstractions;
using SimtekDomain;
using SimtekDomain.Errors;
using SimtekDomain.InterventionCQRS;

namespace SimtekApplication.Handlers.Intervention;

public class
    GetInterventionsQueryHandler : IRequestHandler<GetInterventionsQuery,OneOf<List<InterventionShort>,SimtekError>>
{
    private readonly IInterventionRepository _interventionRepository;


    public GetInterventionsQueryHandler(IInterventionRepository interventionRepository)
    {
        _interventionRepository = interventionRepository;
      
    }

    public async Task<OneOf<List<SimtekDomain.InterventionShort>, SimtekError>> Handle(GetInterventionsQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            var interventionDtos =  await _interventionRepository.GetInterventionsAsync(cancellationToken);

            return interventionDtos
                .Select(dto => dto.ToDomainModel())
                .ToList();
        }
        catch (Exception ex)
        {
            return new SimtekError(new DatabaseError(ex));
        }
    }
}