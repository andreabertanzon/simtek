namespace Simtek.Application.Repositories.Mappings;

public static class OperatorsExtensions
{
    public static Domain.Operator ToDomain(this Data.Worker worker)
    {
        return new Domain.Operator
        {
            Id = worker.Id,
            Name = worker.Name,
            Surname = worker.Surname,
            PricePerHour = worker.PricePerHour
        };
    }
}