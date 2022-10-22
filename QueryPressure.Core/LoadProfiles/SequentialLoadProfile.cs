using QueryPressure.Core.Interfaces;

namespace QueryPressure.LoadProfiles;

/// <summary>
/// Профиль загрузки с последовательным выполнением задач
/// </summary>
public class SequentialLoadProfile : IProfile, IExecutionHook
{
    private TaskCompletionSource? _taskCompletionSource;
    public Task OnQueryExecutedAsync(CancellationToken cancellationToken = default)
    {
        if (_taskCompletionSource == null)
        {
            throw new InvalidOperationException($"{nameof(OnQueryExecutedAsync)} is called before first {nameof(WhenNextCanBeExecutedAsync)} called");
        }

        _taskCompletionSource.SetResult();

        return Task.CompletedTask;
    }

    public async Task WhenNextCanBeExecutedAsync(CancellationToken cancellationToken = default)
    {
        if (_taskCompletionSource != null)
        {
            await _taskCompletionSource.Task;
        }

        _taskCompletionSource = new();
    }
}
