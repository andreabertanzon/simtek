using Microsoft.EntityFrameworkCore;
using Simtek.Application.Repositories.Mappings;
using Simtek.Data;
using Worker = Simtek.Domain.Worker;

namespace Simtek.Application.Repositories;

public class WorkerRepository(IDbContextFactory<SimtekContext> contextFactory):IWorkerRepository
{
    public async Task<IEnumerable<Worker>> GetWorkersAsync(Func<Data.Worker, bool>? predicate = null, CancellationToken cancellationToken = default)
    {
        await using var context = await contextFactory.CreateDbContextAsync(cancellationToken);
        return context.Workers
            .Where(predicate ?? (_ => true))
            .Select(x => x.ToDomain())
            .ToArray();
    }
}