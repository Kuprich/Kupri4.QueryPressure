using QueryPressure.Core.Interfaces;
using QueryPressure.LoadProfiles;

namespace QueryPressure.Core.LoadProfiles;

public class SequentialLoadWithDelayProfile : IProfile, IExecutionHook
{
    private readonly TimeSpan _delay;
    private readonly SequentialLoadProfile _internal;

    public SequentialLoadWithDelayProfile(TimeSpan delay)
    {
        _delay = delay;
        _internal = new SequentialLoadProfile();
    }
    public async Task OnQueryExecutedAsync(CancellationToken cancellationToken = default)
    {
        await Task.Delay(_delay);
        await _internal.OnQueryExecutedAsync(cancellationToken);
    }

    public async Task WhenNextCanBeExecutedAsync(CancellationToken cancellationToken = default)
    {
        await _internal.WhenNextCanBeExecutedAsync(cancellationToken);
    }
}
