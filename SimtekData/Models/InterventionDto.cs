namespace SimtekData.Models;

public class InterventionDto
{
    public int Id { get; set; }
    public int SiteId { get; set; }
    public DateTime InterventionDate { get; set; }
    public bool Stored { get; set; }
}