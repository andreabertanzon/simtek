using MediatR;
using Simtek.Application.Queries;
using Simtek.Application.Repositories;
using Simtek.Domain;
using Simtek.Domain.Helpers;

namespace Simtek.Application.Handlers;

public class GetInterventionsQueryHandler(IInterventionRepository interventionRepository): IRequestHandler<GetInterventionsQuery, OperationResult<IEnumerable<Intervention>>>
{
    public async Task<OperationResult<IEnumerable<Intervention>>> Handle(GetInterventionsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var interventions = await interventionRepository.GetInterventionsAsync(
                request.Page,
                request.PageSize,
                cancellationToken,
                i => i.Site.CustomerId == request.CustomerId,
                request.IncludeCustomer);
            return OperationResult<IEnumerable<Intervention>>.Success(interventions);
        }
        catch (Exception e)
        {
            return OperationResult<IEnumerable<Intervention>>.Failure(e);
        }
        throw new NotImplementedException();
    }
}