namespace SimtekMaui.Application.Infrastructure;

public interface ISimtekService
{
    public Task CacheCustomersAsync(CancellationToken cancellationToken = default);

}