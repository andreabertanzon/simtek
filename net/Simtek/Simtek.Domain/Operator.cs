namespace Simtek.Domain;

public sealed class Operator
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public decimal PricePerHour { get; set; }
    public string FullName => $"{Name} {Surname}";
}