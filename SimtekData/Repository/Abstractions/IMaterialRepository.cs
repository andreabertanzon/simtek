using SimtekData.Models;

namespace SimtekData.Repository.Abstractions;

public interface IMaterialRepository
{
    Task<List<MaterialDto>> GetMaterialsAsync(CancellationToken cancellationToken);
    Task<MaterialDto?> GetMaterialByIdAsync(int id, CancellationToken cancellationToken);
    Task AddMaterialAsync(MaterialDto materialDto, CancellationToken cancellationToken);
    Task UpdateMaterialAsync(MaterialDto materialDto, CancellationToken cancellationToken);
    Task StoreMaterialAsync(int id, CancellationToken cancellationToken);
}