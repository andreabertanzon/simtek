using MediatR;
using Simtek.Application.Queries;
using Simtek.Application.Repositories;
using Simtek.Domain;
using Simtek.Domain.Helpers;

namespace Simtek.Application.Handlers;

public class GetSiteQueryHandler(ISiteRepository siteRepository)
    :IRequestHandler<GetSiteQuery, OperationResult<Site>>
{
    public async Task<OperationResult<Site>> Handle(GetSiteQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var site = await siteRepository.GetSiteByIdAsync(request.Id, cancellationToken);
            return OperationResult<Site>.Success(site);
        }
        catch (Exception e)
        {
            return OperationResult<Site>.Failure(e);
        }
    }
}