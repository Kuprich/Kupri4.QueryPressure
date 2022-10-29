using QueryPressure.Core.LoadProfiles;

namespace QueryPressure.Tests.ProfileTests;

public class TargetThroughputLoadProfileTests
{
    [Fact]
    public void WhenNextCanBeExecutedAsync_FirstCall_Return_CompletedTask()
    {
        var profile = new TargetThroughputLoadProfile(10);
        var task = profile.WhenNextCanBeExecutedAsync();
        Assert.True(task.IsCompleted);
    }

    [Fact]
    public async void WhenNextCanBeExecutedAsync_SecondCall_Return_CompletesTaskOnlyAfterDelay()
    {
        var profile = new TargetThroughputLoadProfile(2);
        _ = profile.WhenNextCanBeExecutedAsync();
        var task = profile.WhenNextCanBeExecutedAsync();

        await Task.Delay(100);
        Assert.False(task.IsCompleted);

        await Task.Delay(500);
        Assert.True(task.IsCompleted);
    }

    [Fact]
    public async void WhenNextCanBeExecutedAsync_IdCalledWithoutDelay_Return_CompletionsAreDistributed_InTime()
    {
        var profile = new TargetThroughputLoadProfile(2);
        _ = profile.WhenNextCanBeExecutedAsync();
        _ = profile.WhenNextCanBeExecutedAsync();
        var task = profile.WhenNextCanBeExecutedAsync();

        await Task.Delay(750);
        Assert.False(task.IsCompleted);

        await Task.Delay(300);
        Assert.True(task.IsCompleted);
    }

}