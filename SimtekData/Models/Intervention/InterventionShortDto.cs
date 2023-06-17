namespace SimtekData.Models.Intervention;

public class InterventionShortDto
{
    public int Id { get; set; }
    public string SiteName { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public double HourSpent { get; set; }
    public double TotalWorkerCost { get; set; } = 0;
    public double? TotalMaterialCost { get; set; } = 0;
}