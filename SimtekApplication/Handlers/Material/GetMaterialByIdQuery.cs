using MediatR;
using OneOf;
using SimtekDomain.Errors;
using SimtekDomain.MaterialCQRS;

namespace SimtekApplication.Handlers.Material;

public class GetMaterialByIdQueryHandler:IRequestHandler<GetMaterialByIdQuery, OneOf<SimtekDomain.Material,SimtekError>>
{
    public Task<OneOf<SimtekDomain.Material, SimtekError>> Handle(GetMaterialByIdQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}