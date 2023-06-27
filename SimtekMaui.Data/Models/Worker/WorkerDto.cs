namespace SimtekMaui.Data.Models.Worker;

public class WorkerDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public double Pph { get; set; }
    public bool Stored { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime LastUpdateDate { get; set; }
}