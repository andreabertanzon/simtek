namespace SimtekMaui.Data.Models;

public class InterventionMaterial
{
    public int InterventionId { get; set; }
    public int MaterialId { get; set; }
    public bool Stored { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime LastUpdateDate { get; set; }
}