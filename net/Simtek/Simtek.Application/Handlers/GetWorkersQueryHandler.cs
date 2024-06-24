using MediatR;
using Simtek.Application.Queries;
using Simtek.Application.Repositories;
using Simtek.Domain;
using Simtek.Domain.Helpers;

namespace Simtek.Application.Handlers;

public class GetWorkersQueryHandler(IWorkerRepository workerRepository)
    : IRequestHandler<GetWorkersQuery, OperationResult<IEnumerable<Worker>>>
{
    public async Task<OperationResult<IEnumerable<Worker>>> Handle(GetWorkersQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            var workers = await workerRepository.GetWorkersAsync(cancellationToken: cancellationToken);
            return OperationResult<IEnumerable<Worker>>.Success(workers);
        }
        catch (Exception e)
        {
            return OperationResult<IEnumerable<Worker>>.Failure(e);
        }
    }
}