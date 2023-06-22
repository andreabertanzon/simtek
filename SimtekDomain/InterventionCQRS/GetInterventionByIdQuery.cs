using MediatR;
using OneOf;
using SimtekDomain.Errors;

namespace SimtekDomain.InterventionCQRS;

public class GetInterventionByIdQuery:IRequest<OneOf<InterventionShort,SimtekError>>
{
    public int Id { get; }
}