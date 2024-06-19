using MediatR;
using Simtek.Application.Commands;
using Simtek.Application.Repositories;
using Simtek.Domain;
using Simtek.Domain.Helpers;

namespace Simtek.Application.Handlers;

public class CreateSiteCommandHandler(ISiteRepository siteRepository): IRequestHandler<CreateSiteCommand, OperationResult<Unit>>
{
    public async Task<OperationResult<Unit>> Handle(CreateSiteCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await siteRepository.CreateSiteAsync(request.Site, cancellationToken);
            return OperationResult<Unit>.Success(Unit.Value);
        }
        catch (Exception e)
        {
            return OperationResult<Unit>.Failure(e);
        }
    }
}