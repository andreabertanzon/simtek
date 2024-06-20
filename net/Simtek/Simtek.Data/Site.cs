namespace Simtek.Data;

public partial class Site
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string? City { get; set; }

    public string? Zip { get; set; }

    public int CustomerId { get; set; }

    public DateTime CreationDate { get; set; }

    public int? LastUpdateDat { get; set; }

    public bool Stored { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual ICollection<Intervention> Interventions { get; set; } = new List<Intervention>();
}
