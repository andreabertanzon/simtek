namespace Simtek.Data;

public partial class Customer
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string Surname { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Phone { get; set; }

    public string? Address { get; set; }

    public string? City { get; set; }

    public string? Zip { get; set; }

    public string? Vat { get; set; }

    public DateTime? Creationdate { get; set; }

    public bool Stored { get; set; }

    public virtual ICollection<Site> Sites { get; set; } = new List<Site>();
}
