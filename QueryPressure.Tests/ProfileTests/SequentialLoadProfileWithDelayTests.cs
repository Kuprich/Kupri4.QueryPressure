using QueryPressure.Core.LoadProfiles;

namespace QueryPressure.Tests.ProfileTests;

public class SequentialLoadProfileWithDelayTests
{
    [Fact]
    public void WhenNextCanBeExecutedAsync_FirstCall_ReturnsCompletedTask()
    {
        var profile = new SequentialLoadWithDelayProfile(TimeSpan.FromMilliseconds(500));
        var task = profile.WhenNextCanBeExecutedAsync();
        Assert.True(task.IsCompletedSuccessfully);
    }

    [Fact]
    public async void WhenNextCanBeExecutedAsync_SecondCall_CompletesOnlyAfter_OnQueryExecutedAsyncCalled_AndDelay()
    {
        var profile = new SequentialLoadWithDelayProfile(TimeSpan.FromMilliseconds(10));
        _ = profile.WhenNextCanBeExecutedAsync();
        var task = profile.WhenNextCanBeExecutedAsync();
        Assert.False(task.IsCompleted);

        await Task.Delay(15);
        Assert.False(task.IsCompleted);

        await profile.OnQueryExecutedAsync();
        Assert.False(task.IsCompleted);

        await Task.Delay(15);
        Assert.True(task.IsCompletedSuccessfully);
    }
}
