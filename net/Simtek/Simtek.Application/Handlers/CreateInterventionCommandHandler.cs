using MediatR;
using Simtek.Application.Commands;
using Simtek.Application.Repositories;
using Simtek.Domain.Helpers;

namespace Simtek.Application.Handlers;

public class CreateInterventionCommandHandler(IInterventionRepository interventionRepository):IRequestHandler<CreateInterventionCommand, OperationResult<Unit>>
{
    public async Task<OperationResult<Unit>> Handle(CreateInterventionCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await interventionRepository.CreateInterventionAsync(request.Intervention, cancellationToken);
            return OperationResult<Unit>.Success(Unit.Value);
        }
        catch (Exception e)
        {
            return OperationResult<Unit>.Failure(e);
        }
    }
}