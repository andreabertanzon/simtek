using MediatR;
using Simtek.Domain.Helpers;

namespace Simtek.Application.Commands;

public class GenerateExcelCommand:IRequest<OperationResult<Stream>>
{
    public required int InterventionId { get; set; }
}