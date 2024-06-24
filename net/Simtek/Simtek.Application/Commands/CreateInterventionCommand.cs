using MediatR;
using Simtek.Domain;
using Simtek.Domain.Helpers;

namespace Simtek.Application.Commands;

public class CreateInterventionCommand:IRequest<OperationResult<Unit>>
{
    public required Intervention Intervention { get; set; }
}