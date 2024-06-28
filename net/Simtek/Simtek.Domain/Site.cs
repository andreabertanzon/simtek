namespace Simtek.Domain;

public sealed class Site
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public string Address { get; set; }
    public string? City { get; set; }
    public string? ZipCode { get; set; }
    
    public int CustomerId { get; set; }
    
    public string? CustomerName { get; set; }
}