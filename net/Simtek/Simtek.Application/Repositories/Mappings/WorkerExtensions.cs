namespace Simtek.Application.Repositories.Mappings;

public static class WorkerExtensions
{
    public static Domain.Worker ToDomain(this Data.Worker worker)
    {
        return new Domain.Worker
        {
            Id = worker.Id,
            Name = worker.Name,
            Surname = worker.Surname,
            PricePerHour = worker.PricePerHour
        };
    }
}