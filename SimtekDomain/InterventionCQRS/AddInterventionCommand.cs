using MediatR;
using OneOf;
using SimtekDomain.Errors;

namespace SimtekDomain.InterventionCQRS;

public record AddInterventionCommand(Intervention Intervention):IRequest<OneOf<Unit,SimtekError>>
{
    
}