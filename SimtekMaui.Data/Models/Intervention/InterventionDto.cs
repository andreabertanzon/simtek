namespace SimtekMaui.Data.Models.Intervention;

public class InterventionDto
{
    public Guid Id { get; set; }
    public Guid SiteId { get; set; }
    public string SiteName { get; set; }
    public string SiteAddress { get; set; }
    public string CustomerName { get; set; }
    public string CustomerSurname { get; set; }
    public string CustomerAddress { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateOnly InterventionDate { get; set; }
    public bool Stored { get; set; }
    
}