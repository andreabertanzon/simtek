namespace Simtek.Domain;

public sealed class Customer
{
    public required int Id { get; set; }
    public string Name { get; set; }
    public required string Surname { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string VatNumber { get; set; }
    public string ZipCode { get; set; }
    
    public string FullName => $"{Name} {Surname}";

    public IEnumerable<Site> Sites { get; set; } = [];
}