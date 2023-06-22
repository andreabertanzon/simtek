using SimtekData.Models;
using SimtekData.Models.Intervention;
using SimtekDomain;

namespace SimtekData.Repository.Abstractions;

public interface IInterventionRepository
{
    Task<IEnumerable<InterventionShortDto>> GetInterventionsAsync(CancellationToken cancellationToken = default);

    Task<InterventionShortDto?> GetInterventionByIdAsync(int id,
        CancellationToken cancellationToken = default);

    Task AddInterventionAsync(Intervention intervention, CancellationToken cancellationToken);
    void DeleteIntervention(int id);
}