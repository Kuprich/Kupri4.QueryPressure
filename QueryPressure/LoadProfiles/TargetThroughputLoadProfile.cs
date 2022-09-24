using QueryPressure.Interfaces;

namespace QueryPressure.LoadProfiles;

/// <summary>
/// Профиль загрузки с возможностью задавать нагрузку в виде кол-ва задач в секунду
/// </summary>
public class TargetThroughputLoadProfile : IProfile
{
    private readonly TimeSpan _delay;
    private DateTime? _nextExecution; 

    public TargetThroughputLoadProfile(int targetRPS)
    {
        _delay = TimeSpan.FromMilliseconds(1000f / targetRPS);
    }
    public Task OnQueryExecutedAsync(CancellationToken cancellationToken = default) => Task.CompletedTask;

    public async Task<bool> WhenNextCanBeExecutedAsync(CancellationToken cancellationToken = default)
    {
        var now = DateTime.Now;

        if (_nextExecution == null || now > _nextExecution)
        {
            _nextExecution = now + _delay;
        }
        else
        {
            var delta = _nextExecution.Value - now;
            _nextExecution += _delay;
            await Task.Delay(delta, cancellationToken);
        }

        return cancellationToken.IsCancellationRequested;

    }
}