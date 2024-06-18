using System.ComponentModel.DataAnnotations;

namespace Simtek.Domain;

public sealed class Customer
{
    public required int Id { get; set; }
    public string? Name { get; set; }
    [Required]
    [MinLength(3)]
    public required string Surname { get; set; }
    [EmailAddress]
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? Vat { get; set; }
    public string? Zip { get; set; }
    
    public string FullName => $"{Name} {Surname}";

    public IEnumerable<Site> Sites { get; set; } = [];
}