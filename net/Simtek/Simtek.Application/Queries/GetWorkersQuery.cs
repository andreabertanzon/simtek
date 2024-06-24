using MediatR;
using Simtek.Domain;
using Simtek.Domain.Helpers;

namespace Simtek.Application.Queries;

public class GetWorkersQuery:IRequest<OperationResult<IEnumerable<Worker>>>
{
    
}