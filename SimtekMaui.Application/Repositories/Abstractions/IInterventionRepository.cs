using SimtekMaui.Data.Models.Intervention;

namespace SimtekMaui.Data.Repositories.Abstractions;

public interface IInterventionRepository
{
    Task<List<InterventionDto>> GetAsync(CancellationToken cancellationToken);
}