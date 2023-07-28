namespace SimtekMaui.Data.Models;

public class SiteDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public Guid CustomerId { get; set; }
    public bool Stored { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime LastUpdateDate { get; set; }
}