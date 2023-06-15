namespace SimtekData.Models;

public class SiteDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public int CustomerId { get; set; }
    public bool Stored { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime LastUpdateDate { get; set; }
}