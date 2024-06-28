using Microsoft.EntityFrameworkCore;
using Simtek.Application.Repositories.Mappings;
using Simtek.Data;
using Simtek.Domain;
using Site = Simtek.Domain.Site;

namespace Simtek.Application.Repositories;

public class SiteRepository(
    IDbContextFactory<SimtekContext> contextFactory
    ):ISiteRepository
{
    public async Task<IEnumerable<Site>> GetSitesAsync(Func<Data.Site, bool>? predicate = null, CancellationToken cancellationToken = default)
    {
        await using var context = await contextFactory.CreateDbContextAsync(cancellationToken);
        var sites = context
            .Sites
            .Include(x=>x.Customer)
            .Where(predicate ?? (_ => true)).ToArray();

        return sites.ToDomain();
    }

    public async Task<Site> GetSiteByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        await using var context = await contextFactory.CreateDbContextAsync(cancellationToken);
        var site = await context
            .Sites
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        
        return site?.ToDomainWithNoIntervention() ?? throw new NoDataFoundException("Site not found");
    }

    public async Task CreateSiteAsync(Site site, CancellationToken cancellationToken = default)
    {
        await using var context = await contextFactory.CreateDbContextAsync(cancellationToken);
        
        if (context.Sites.Any(x => x.Name == site.Name))
        {
            context.Sites.Update(site.ToData());
            await context.SaveChangesAsync(cancellationToken);
            return;
        }
        
        context.Sites.Add(site.ToData());
        await context.SaveChangesAsync(cancellationToken);
    }

    public Task DeleteSiteAsync(int id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}