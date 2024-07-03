using MediatR;
using Simtek.Domain;
using Simtek.Domain.Helpers;

namespace Simtek.Application.Commands;

public class UpdateCustomerCommand: IRequest<OperationResult<Unit>>
{
    public required Customer Customer { get; set; }
}