using MediatR;
using Npgsql;
using OneOf;
using OneOf.Types;
using SimtekApplication.Handlers.Mappers;
using SimtekData;
using SimtekData.Repository;
using SimtekDomain;
using SimtekDomain.Errors;
using SimtekDomain.InterventionCQRS;

namespace SimtekApplication.Handlers.Intervention;

public class
    GetInterventionsQueryHandler : IRequestHandler<GetInterventionsQuery,
        OneOf<List<SimtekDomain.Intervention>, SimtekError>>
{
    private readonly InterventionRepository _interventionRepository;


    public GetInterventionsQueryHandler(InterventionRepository interventionRepository)
    {
        _interventionRepository = interventionRepository;
      
    }

    public async Task<OneOf<List<SimtekDomain.Intervention>, SimtekError>> Handle(GetInterventionsQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            var fullInterventionDtos =  await _interventionRepository.GetFullInterventions(cancellationToken);

            return fullInterventionDtos
                .Select(dto => dto.ToDomainModel())
                .ToList();
        }
        catch (Exception ex)
        {
            return new SimtekError(new DatabaseError(ex));
        }
    }
}