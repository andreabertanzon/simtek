using MediatR;
using OneOf;
using SimtekDomain;
using SimtekDomain.Errors;
using SimtekDomain.InterventionCQRS;
using SimtekMaui.Application.Mappers;
using SimtekMaui.Data.Repositories;

namespace SimtekMaui.Application.Handlers;

public class GetInterventionsQueryHandler:IRequestHandler<GetInterventionsQuery, OneOf<List<Intervention>,SimtekError>>
{
    private readonly InterventionRepository _interventionRepository;

    public GetInterventionsQueryHandler(InterventionRepository interventionRepository)
    {
        _interventionRepository = interventionRepository;
    }
    public async Task<OneOf<List<Intervention>, SimtekError>> Handle(GetInterventionsQuery request, CancellationToken cancellationToken)
    {
        var interventionsDto = await _interventionRepository.GetAsync();
        return interventionsDto.Select(x=>x.ToDomainModel()).ToList();
    }
}