using SimtekData.Models.Intervention;

namespace SimtekData.Models;

public class FullInterventionDto
{
    public required InterventionShortDto InterventionShortDto { get; set; }
    public required List<MaterialDto> MaterialDto { get; set; }
    public required SiteDto SiteDto { get; set; }
    public required List<WorkerDto> WorkerDto { get; set; }
}