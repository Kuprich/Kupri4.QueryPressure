using QueryPressure.Core.Interfaces;

namespace QueryPressure.Core.LoadProfiles;

/// <summary>
/// Профиль загрузки с последовательным выполнением задач
/// </summary>
public class SequentionalLoadProfile : IProfile
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

    public async Task<bool> WhenNextCanBeExecutedAsync(CancellationToken cancellationToken = default)
    {
        if (_taskCompletionSource != null)
        {
            await _taskCompletionSource.Task;
        }

        _taskCompletionSource = new();

        var result = cancellationToken.IsCancellationRequested;

        return result;

    }
}
