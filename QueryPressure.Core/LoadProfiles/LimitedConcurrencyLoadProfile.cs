using QueryPressure.Core.Interfaces;

namespace QueryPressure.Core.LoadProfiles;

public class LimitedConcurrencyLoadProfile : IProfile
{
    private readonly int _limit;
    private readonly SemaphoreSlim _semaphore;

    public LimitedConcurrencyLoadProfile(int limit)
    {
        _limit = limit;
        _semaphore = new SemaphoreSlim(limit);
    }
    public Task OnQueryExecutedAsync(CancellationToken cancellationToken = default)
    {
        _semaphore.Release();
        return Task.CompletedTask;
    }

    public async Task<bool> WhenNextCanBeExecutedAsync(CancellationToken cancellationToken = default)
    {
        await _semaphore.WaitAsync(cancellationToken);
        return cancellationToken.IsCancellationRequested;
    }
}
