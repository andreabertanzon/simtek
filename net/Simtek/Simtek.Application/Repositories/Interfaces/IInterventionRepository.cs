using Simtek.Domain;

namespace Simtek.Application.Repositories;

public interface IInterventionRepository
{
    public Task<IEnumerable<Intervention>> GetInterventionsAsync(int page, int pageSize,
        CancellationToken cancellationToken = default, Func<Data.Intervention, bool>? predicate = null,
        bool includeCustomer = false);
    public Task<Intervention> GetInterventionAsync(int id, CancellationToken cancellationToken = default);
    public Task<Intervention> CreateInterventionAsync(Intervention intervention, CancellationToken cancellationToken = default);
    public Task<Intervention> UpdateInterventionAsync(Intervention intervention, CancellationToken cancellationToken = default);
    public Task<Intervention> DeleteInterventionAsync(int id, CancellationToken cancellationToken = default);
}