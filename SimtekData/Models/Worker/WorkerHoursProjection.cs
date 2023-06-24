using SimtekDomain.MaterialCQRS;

namespace SimtekData.Models.Worker;

public class WorkerHoursProjection
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public double HourWorked { get; set; }
    public double Pph { get; set; }

    public double WorkerTotal => HourWorked * Pph;
}