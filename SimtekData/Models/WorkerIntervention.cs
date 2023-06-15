namespace SimtekData.Models;

public class WorkerIntervention
{
    public int WorkerId { get; set; }
    public int InterventionId { get; set; }
    public double HoursWorked { get; set; }
    public bool Stored { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime LastUpdateDate { get; set; }
}