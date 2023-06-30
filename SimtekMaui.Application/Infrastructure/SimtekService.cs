namespace SimtekMaui.Application.Infrastructure;

public class SimtekService:ISimtekService
{
    public Task CacheCustomersAsync(CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }
}