namespace Simtek.Domain;

public sealed class Worker
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public decimal PricePerHour { get; set; }
    public string FullName => $"{Name} {Surname}";
    
    // two workers are equal if they have the same id
    public override bool Equals(object? obj) => obj is Worker worker && worker.Id == Id;

    private bool Equals(Worker other)
    {
        return Id == other.Id;
    }
}