using MediatR;
using OneOf;
using OneOf.Types;
using SimtekDomain.Errors;

namespace SimtekDomain.Query;

public class GetInterventions:IRequest<OneOf<List<Intervention>,NotFoundError>>, IRequest<OneOf<List<Intervention>, SimtekError>>
{
    
}