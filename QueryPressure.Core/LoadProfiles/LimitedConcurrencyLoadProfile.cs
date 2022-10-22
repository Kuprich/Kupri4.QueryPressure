using QueryPressure.Core.Interfaces;

namespace QueryPressure.Core.LoadProfiles;

public class LimitedConcurrencyLoadProfile : IProfile, IExecutionHook
{

    private readonly SemaphoreSlim _semaphore;

    public LimitedConcurrencyLoadProfile(int limit)
    {
        _semaphore = new SemaphoreSlim(limit);
    }
    public Task OnQueryExecutedAsync(CancellationToken cancellationToken = default)
    {
        _semaphore.Release();
        return Task.CompletedTask;
    }

    public async Task WhenNextCanBeExecutedAsync(CancellationToken cancellationToken = default)
    {
        await _semaphore.WaitAsync(cancellationToken);
    }
}
