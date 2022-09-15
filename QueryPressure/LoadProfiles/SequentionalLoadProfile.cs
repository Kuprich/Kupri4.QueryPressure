using QueryPressure.Interfaces;

namespace QueryPressure.LoadProfiles;

public class SequentionalLoadProfile : IProfile
{
    public Task<bool> WhenNextCanBeExecuted(CancellationToken cancellationToken)
    {
        return Task.FromResult(cancellationToken.IsCancellationRequested);
    }
}