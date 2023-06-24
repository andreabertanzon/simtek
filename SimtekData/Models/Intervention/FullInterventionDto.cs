using SimtekData.Models.Intervention;
using SimtekData.Models.Worker;

namespace SimtekData.Models;

public class FullInterventionDto
{
    public required InterventionShortDto InterventionShortDto { get; set; }
    public required List<MaterialDto> MaterialDto { get; set; }
    public required SiteDto SiteDto { get; set; }
    public required CustomerDto CustomerDto { get; set; }
    public required List<WorkerHoursProjection> WorkerDto { get; set; }
}