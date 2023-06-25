using MediatR;
using OneOf;
using SimtekDomain.Errors;

namespace SimtekDomain.InterventionCQRS;

public record GetInterventionByIdQuery(int Id):IRequest<OneOf<Intervention,SimtekError>>{}