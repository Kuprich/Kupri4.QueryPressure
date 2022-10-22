using QueryPressure.Core.Interfaces;

namespace QueryPressure.Core.LoadProfiles;

public class LimitedConcurrencyWithDelayLoadProfile : IProfile
{
    private readonly TimeSpan _delay;
    private readonly LimitedConcurrencyLoadProfile _internal;

    public LimitedConcurrencyWithDelayLoadProfile(int limit, TimeSpan delay)
    {
        _delay = delay;
        _internal = new LimitedConcurrencyLoadProfile(limit);
    }
    public async Task OnQueryExecutedAsync(CancellationToken cancellationToken)
    {
        await Task.Delay(_delay);
        await _internal.OnQueryExecutedAsync(cancellationToken);
    }

    public async Task WhenNextCanBeExecutedAsync(CancellationToken cancellationToken)
    {
        await _internal.WhenNextCanBeExecutedAsync(cancellationToken);
    }
}
