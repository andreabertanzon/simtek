using SimtekData.Models;
using SimtekData.Models.Intervention;
using SimtekDomain;

namespace SimtekData.Repository.Abstractions;

public interface IInterventionRepository
{
    Task<IEnumerable<InterventionShortDto>> GetAsync(CancellationToken cancellationToken = default);

    Task<FullInterventionDto?> GetByIdAsync(int id,
        CancellationToken cancellationToken = default);

    Task AddAsync(Intervention intervention, CancellationToken cancellationToken);
    void DeleteAsync(int id);
}