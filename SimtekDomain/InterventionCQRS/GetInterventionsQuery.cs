using MediatR;
using OneOf;
using SimtekDomain.Errors;

namespace SimtekDomain.InterventionCQRS;

public class GetInterventionsQuery:IRequest<OneOf<List<Intervention>,NotFoundError>>, IRequest<OneOf<List<Intervention>, SimtekError>>
{
    
}