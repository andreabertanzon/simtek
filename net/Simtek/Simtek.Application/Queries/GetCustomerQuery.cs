using MediatR;
using Simtek.Domain;
using Simtek.Domain.Helpers;

namespace Simtek.Application.Queries;

public class GetCustomerQuery:IRequest<OperationResult<Customer>>
{
    public required int Id { get; set; }
}