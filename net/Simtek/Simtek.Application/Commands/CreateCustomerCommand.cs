using MediatR;
using Simtek.Domain;
using Simtek.Domain.Helpers;

namespace Simtek.Application.Commands;

public class CreateCustomerCommand:IRequest<OperationResult<Unit>>
{
    public required Customer Customer { get; set; }
}