using MediatR;
using SimtekMaui.Application.Mappers;
using SimtekMaui.Data.Repositories.Abstractions;
using SimtekMaui.Models;
using SimtekMaui.Models.Query;

namespace SimtekMaui.Application.Handlers;

public class GetInterventionsQueryHandler:IRequestHandler<GetInterventionsQuery,Result<List<Intervention>>>
{
    private readonly IInterventionRepository _interventionRepository;

    public GetInterventionsQueryHandler(IInterventionRepository interventionRepository)
    {
        _interventionRepository = interventionRepository;
    }
    public async Task<Result<List<Intervention>>> Handle(GetInterventionsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var interventionsDto = await _interventionRepository.GetAsync(cancellationToken);
            var result = interventionsDto.Select(x => x.ToDomainModel()).ToList();

            return Result<List<Intervention>>.Success(result);
        }catch(Exception ex)
        {
            return Result<List<Intervention>>.Failure(ex);
        }
    }
}