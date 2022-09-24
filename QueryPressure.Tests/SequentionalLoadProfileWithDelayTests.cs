using QueryPressure.LoadProfiles;
using Xunit;

namespace QueryPressure.Tests;

public class SequentionalLoadProfileWithDelayTests
{
    [Fact]
    public void WhenNextCanBeExecutedAsync_FirstCall_ReturnsCompletedTask()
    {
        var profile = new SequentionalLoadProfileWithDelay(TimeSpan.FromMilliseconds(500));
        var task = profile.WhenNextCanBeExecutedAsync();
        Assert.True(task.IsCompletedSuccessfully);
    }

    [Fact]
    public async void WhenNextCanBeExecutedAsync_SecondCall_CompletesOnlyAfter_OnQueryExecutedAsyncCalled_AndDelay()
    {
        var profile = new SequentionalLoadProfileWithDelay(TimeSpan.FromMilliseconds(10));
        _ = profile.WhenNextCanBeExecutedAsync();
        var task = profile.WhenNextCanBeExecutedAsync();

        await Task.Delay(15);
        Assert.False(task.IsCompleted);

        await profile.OnQueryExecutedAsync();
        await Task.Delay(10);
        Assert.False(task.IsCompleted);

        await Task.Delay(15);
        Assert.True(task.IsCompletedSuccessfully);
    }
}
