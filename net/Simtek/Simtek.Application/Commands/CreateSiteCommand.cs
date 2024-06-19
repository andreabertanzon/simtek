using MediatR;
using Simtek.Domain;
using Simtek.Domain.Helpers;

namespace Simtek.Application.Commands;

public class CreateSiteCommand: IRequest<OperationResult<Unit>>
{
    public required Site Site { get; set; }
}