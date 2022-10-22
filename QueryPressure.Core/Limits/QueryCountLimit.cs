using QueryPressure.Core.Interfaces;

namespace QueryPressure.Core.Limits;

public class QueryCountLimit : ILimit, IExecutionHook
{

    private readonly CancellationTokenSource toketSource;
    private readonly int _count;

    private int _currentCount;

    public QueryCountLimit(int count)
    {
        toketSource = new();
        _count = count;
    }

    public CancellationToken Token => toketSource.Token;

    public Task OnQueryExecutedAsync(CancellationToken cancellationToken)
    {
        _currentCount++;

        if (_currentCount > _count)
        {
            toketSource.Cancel();
        }

        return Task.CompletedTask;
    }
}
