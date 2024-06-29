using MediatR;
using Simtek.Application.Queries;
using Simtek.Application.Repositories;
using Simtek.Domain;
using Simtek.Domain.Helpers;

namespace Simtek.Application.Handlers;

public class GetInterventionQueryHandler(IInterventionRepository interventionRepository): IRequestHandler<GetInterventionQuery, OperationResult<Intervention>>
{
    public async Task<OperationResult<Intervention>> Handle(GetInterventionQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var intervention =
                await interventionRepository.GetInterventionsAsync(0, 1, cancellationToken, x => x.Id == request.Id,
                    true);
            var interventionToReturn = intervention.First();
            return OperationResult<Intervention>.Success(intervention.First());
        }
        catch (Exception e)
        {
            return OperationResult<Intervention>.Failure(e);
        }
    }
}