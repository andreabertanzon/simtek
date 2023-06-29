namespace SimtekMaui.Data.Models.Intervention;

public class InterventionDto
{
    public int Id { get; set; }
    public int SiteId { get; set; }
    public string SiteName { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime InterventionDate { get; set; }
    public bool Stored { get; set; }
    public int IdentityId => Id;
    
}