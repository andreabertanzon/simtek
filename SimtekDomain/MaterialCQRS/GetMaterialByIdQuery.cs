using MediatR;
using OneOf;
using SimtekDomain.Errors;

namespace SimtekDomain.MaterialCQRS;

public class GetMaterialByIdQuery:IRequest<OneOf<Material,SimtekError>>
{
    public GetMaterialByIdQuery(int id)
    {
        Id = id;
    }

    public int Id { get; init; }
}