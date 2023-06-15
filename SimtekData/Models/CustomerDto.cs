namespace SimtekData.Models;

public class CustomerDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Vat { get; set; }
    public string Email { get; set; }
    public bool Stored { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime LastUpdateDate { get; set; }
}