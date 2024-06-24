namespace Simtek.Domain;

public sealed class Worker
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public decimal PricePerHour { get; set; }
    public string FullName => $"{Name} {Surname}";
}