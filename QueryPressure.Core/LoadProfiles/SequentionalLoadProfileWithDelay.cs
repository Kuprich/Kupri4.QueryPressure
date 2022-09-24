using QueryPressure.Core.Interfaces;

namespace QueryPressure.Core.LoadProfiles;

public class SequentionalLoadProfileWithDelay : IProfile
{
    private readonly TimeSpan _delay;
    private readonly IProfile _profile;
    private DateTime? _netExecution;

    public SequentionalLoadProfileWithDelay(TimeSpan delay)
    {
        _delay = delay;
        _profile = new SequentionalLoadProfile();
    }
    public async Task OnQueryExecutedAsync(CancellationToken cancellationToken = default)
    {
        await _profile.OnQueryExecutedAsync(cancellationToken);
        _netExecution = DateTime.Now + _delay;
    }

    public async Task<bool> WhenNextCanBeExecutedAsync(CancellationToken cancellationToken = default)
    {
        var result = await _profile.WhenNextCanBeExecutedAsync(cancellationToken);
        var now = DateTime.Now;

        if (_netExecution != null && now < _netExecution)
        {
            await Task.Delay(_netExecution.Value - now, cancellationToken);
        }

        return result;
    }
}
