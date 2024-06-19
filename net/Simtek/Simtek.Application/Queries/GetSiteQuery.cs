using MediatR;
using Simtek.Domain;
using Simtek.Domain.Helpers;

namespace Simtek.Application.Queries;

public class GetSiteQuery:IRequest<OperationResult<Site>>
{
    public required int Id { get; set; }
}