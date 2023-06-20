using SimtekData.Models;
using SimtekData.Models.Intervention;
using SimtekDomain;

namespace SimtekData.Repository.Abstractions;

public interface IInterventionRepository
{
    IEnumerable<InterventionShortDto> GetShortInterventions();

    Task<InterventionShortDto?> GetShortInterventionByIdAsync(int id,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<FullInterventionDto>> GetFullInterventions(CancellationToken cancellationToken);

    Task<FullInterventionDto?> GetFullInterventionById(int id,
        CancellationToken cancellationToken = default);

    Task AddInterventionAsync(Intervention intervention, CancellationToken cancellationToken);
    void DeleteIntervention(int id);
}