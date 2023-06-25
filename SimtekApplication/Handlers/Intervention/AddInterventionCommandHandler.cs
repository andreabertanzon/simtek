using MediatR;
using OneOf;
using SimtekData.Repository.Abstractions;
using SimtekDomain.Errors;
using SimtekDomain.InterventionCQRS;

namespace SimtekApplication.Handlers.Intervention;

public class AddInterventionCommandHandler:IRequestHandler<AddInterventionCommand, OneOf<Unit,SimtekError>>
{
    private readonly IInterventionRepository _interventionRepository;

    public AddInterventionCommandHandler(IInterventionRepository interventionRepository)
    {
        _interventionRepository = interventionRepository;
    }
    public async Task<OneOf<Unit, SimtekError>> Handle(AddInterventionCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await _interventionRepository.AddInterventionAsync(request.Intervention, cancellationToken);
            return new Unit();
        }
        catch (Exception e)
        {
            return new SimtekError(new DatabaseError(e));
        }
    }
}