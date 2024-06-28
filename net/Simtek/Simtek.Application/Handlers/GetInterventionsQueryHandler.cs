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
            Func<Data.Intervention, bool>? predicate = null;
            
            if (request.CustomerId is not null)
            {
                predicate = i => i.Site.CustomerId == request.CustomerId;
            }
            else if (request.StartDate is not null && request.EndDate is not null)
            {
                predicate = x=> x.Date >= request.StartDate && x.Date <= request.EndDate;
            }
            else if (request.CustomerId is not null && request.StartDate is not null && request.EndDate is not null)
            {
                predicate = x=> x.Site.CustomerId == request.CustomerId && x.Date >= request.StartDate && x.Date <= request.EndDate;
            }
            var interventions = await interventionRepository.GetInterventionsAsync(
                request.Page,
                request.PageSize,
                cancellationToken,
                predicate,
                request.IncludeCustomer);
            return OperationResult<IEnumerable<Intervention>>.Success(interventions);
        }
        catch (Exception e)
        {
            return OperationResult<IEnumerable<Intervention>>.Failure(e);
        }
    }
}