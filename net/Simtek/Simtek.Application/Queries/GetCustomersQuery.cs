using MediatR;
using Simtek.Domain;
using Simtek.Domain.Helpers;

namespace Simtek.Application.Queries;

public class GetCustomersQuery:IRequest<OperationResult<IEnumerable<Customer>>>
{
    public Func<Data.Customer, bool>? Predicate { get; set; }
}