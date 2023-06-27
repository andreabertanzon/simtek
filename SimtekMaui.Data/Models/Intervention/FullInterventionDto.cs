using SimtekMaui.Data.Models.Material;
using SimtekMaui.Data.Models.Worker;

namespace SimtekMaui.Data.Models.Intervention;

public class FullInterventionDto
{
    public required InterventionShortDto InterventionShortDto { get; set; }
    public required List<MaterialDto> MaterialDto { get; set; }
    public required SiteDto SiteDto { get; set; }
    public required CustomerDto CustomerDto { get; set; }
    public required List<WorkerHoursProjection> WorkerDto { get; set; }
}