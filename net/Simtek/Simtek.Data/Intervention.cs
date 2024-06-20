namespace Simtek.Data;

public partial class Intervention
{
    public int Id { get; set; }

    public DateTime Date { get; set; }

    public string Description { get; set; } = null!;

    public string? Notes { get; set; }

    public int SiteId { get; set; }

    public DateTime? CreationDate { get; set; }

    public DateTime? LastUpdateDate { get; set; }

    public bool Stored { get; set; }

    public string? Material { get; set; }

    public virtual ICollection<InterventionWorker> InterventionWorkers { get; set; } = new List<InterventionWorker>();

    public virtual Site Site { get; set; } = null!;
}
