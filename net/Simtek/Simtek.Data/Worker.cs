namespace Simtek.Data;

public partial class Worker
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public decimal PricePerHour { get; set; }

    public virtual ICollection<InterventionWorker> InterventionWorkers { get; set; } = new List<InterventionWorker>();
}
