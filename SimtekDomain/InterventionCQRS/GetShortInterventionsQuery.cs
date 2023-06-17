using MediatR;
using OneOf;
using SimtekDomain.Errors;

namespace SimtekDomain.InterventionCQRS;

public class GetShortInterventionsQuery : IRequest<OneOf<List<InterventionShort>, SimtekError>>
{
}