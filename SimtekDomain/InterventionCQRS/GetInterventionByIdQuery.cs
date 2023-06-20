using MediatR;
using OneOf;
using SimtekDomain.Errors;

namespace SimtekDomain.InterventionCQRS;

public class GetInterventionByIdQuery:IRequest<OneOf<Intervention,SimtekError>>
{
    public int Id { get; }
}