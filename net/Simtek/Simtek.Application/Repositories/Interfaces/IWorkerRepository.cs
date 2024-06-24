namespace Simtek.Application.Repositories;

public interface IWorkerRepository
{
    public Task<IEnumerable<Domain.Worker>> GetWorkersAsync(Func<Data.Worker, bool>? predicate = null,
        CancellationToken cancellationToken = default);
}