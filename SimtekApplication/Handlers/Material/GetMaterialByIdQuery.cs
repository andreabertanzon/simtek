using MediatR;
using OneOf;
using SimtekApplication.Handlers.Mappers;
using SimtekData.Repository.Abstractions;
using SimtekDomain.Errors;
using SimtekDomain.MaterialCQRS;

namespace SimtekApplication.Handlers.Material;

public class GetMaterialByIdQueryHandler:IRequestHandler<GetMaterialByIdQuery, OneOf<SimtekDomain.Material,SimtekError>>
{
    private readonly IMaterialRepository _materialRepository;

    public GetMaterialByIdQueryHandler(IMaterialRepository materialRepository)
    {
        _materialRepository = materialRepository;
    }
    public async Task<OneOf<SimtekDomain.Material, SimtekError>> Handle(GetMaterialByIdQuery request, CancellationToken cancellationToken)
    {
        var materialDto = await _materialRepository.GetMaterialByIdAsync(request.Id,cancellationToken:cancellationToken);

        return materialDto is null
            ? new SimtekError(new NotFoundError())
            : materialDto.ToDomainModel();
    }
}