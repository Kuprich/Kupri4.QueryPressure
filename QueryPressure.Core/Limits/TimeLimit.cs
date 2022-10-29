using QueryPressure.Core.Interfaces;

namespace QueryPressure.Core.Limits;

public class TimeLimit : ILimit
{
    private readonly CancellationTokenSource _tokenSource;
    public TimeLimit(TimeSpan limit)
    {
        _tokenSource = new(limit);
    }
    public CancellationToken Token => _tokenSource.Token;
}
