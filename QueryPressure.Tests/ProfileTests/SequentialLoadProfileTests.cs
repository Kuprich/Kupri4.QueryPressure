using QueryPressure.Core.LoadProfiles;

namespace QueryPressure.Tests.ProfileTests;

public class SequentialLoadProfileTests
{
    [Fact]
    public void SequentialLoadProfile_FirstCall_ReturnsCompletedTask()
    {
        var profile = new SequentialLoadProfile();
        var task = profile.WhenNextCanBeExecutedAsync();
        Assert.True(task.IsCompletedSuccessfully);
    }

    [Fact]
    public async Task SequentialLoadProfile_SecondCall_ReturnsCompletedTask()
    {
        var profile = new SequentialLoadProfile();
        _ = profile.WhenNextCanBeExecutedAsync();

        var task = profile.WhenNextCanBeExecutedAsync();
        await Task.Delay(10);
        Assert.False(task.IsCompleted);

        await profile.OnQueryExecutedAsync();
        await Task.Delay(10);
        Assert.True(task.IsCompletedSuccessfully);
    }
}
