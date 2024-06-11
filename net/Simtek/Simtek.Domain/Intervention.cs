namespace Simtek.Domain;

public sealed class Intervention
{
    public required int Id { get; set; }
    public required DateTime Date { get; set; } = DateTime.Now;
    public IEnumerable<string> Description { get; set; } = [];
    public string? Notes { get; set; }
    public bool ReportGenerated { get; set; } = false;
    public Site Site { get; set; }
    public IEnumerable<Operator> Operators { get; set; } = [];
    public Dictionary<string, double> Materials { get; set; } = new();
}