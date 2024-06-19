namespace Simtek.Application.Repositories;

public interface ISiteRepository
{
    public Task<IEnumerable<Domain.Site>> GetSitesAsync(Func<Data.Site, bool>? predicate = null,
        CancellationToken cancellationToken = default);
    
    public Task<Domain.Site> GetSiteByIdAsync(int id, CancellationToken cancellationToken = default);
    
    public Task CreateSiteAsync(Domain.Site site, CancellationToken cancellationToken = default);
    
    public Task DeleteSiteAsync(int id, CancellationToken cancellationToken = default);
}