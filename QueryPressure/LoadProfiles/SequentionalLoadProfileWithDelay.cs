using QueryPressure.Interfaces;

namespace QueryPressure.LoadProfiles;

public class SequentionalLoadProfileWithDelay : IProfile
{
    private readonly TimeSpan _delaySpan;

    public SequentionalLoadProfileWithDelay(TimeSpan delaySpan)
    {
        _delaySpan = delaySpan;
    }
    public async Task<bool> WhenNextCanBeExecuted(CancellationToken cancellationToken)
    {
        await Task.Delay(_delaySpan, cancellationToken);
        return cancellationToken.IsCancellationRequested;
    }
}
