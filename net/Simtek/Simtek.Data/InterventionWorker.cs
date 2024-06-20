namespace Simtek.Data;

public partial class InterventionWorker
{
    public int Id { get; set; }

    public int WorkerId { get; set; }

    public decimal HourSpent { get; set; }

    public int InterventionId { get; set; }

    public virtual Intervention Intervention { get; set; } = null!;

    public virtual Worker Worker { get; set; } = null!;
}
