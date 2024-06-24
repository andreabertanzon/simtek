using System.ComponentModel.DataAnnotations;

namespace Simtek.Domain;

public sealed class Intervention
{
    public required int Id { get; set; }
    [Required]
    public required DateTime Date { get; set; } = DateTime.Now;
    [Required]
    public string Description { get; set; } = string.Empty;
    public string? Notes { get; set; }
    public bool ReportGenerated { get; set; } = false;
    public required Site Site { get; set; }
    public Dictionary<Worker, decimal> Operators { get; set; } = [];
    public List<Material> Materials { get; set; } = new();
    
    public string ItalianDate => Date.ToString("dd/MM/yyyy");
}

public class Material
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string UnitMeasure { get; set; }
    public double Quantity { get; set; }
}