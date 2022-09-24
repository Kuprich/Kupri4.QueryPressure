using QueryPressure.LoadProfiles;

namespace QueryPressure.Tests;

public class SequentionalLoadProfileTests
{
    [Fact]
    public void SequentionalLoadProfile_FirstCall_ReturnsCompletedTask()
    {
        var profile = new SequentionalLoadProfile();
        var task = profile.WhenNextCanBeExecutedAsync();
        Assert.True(task.IsCompletedSuccessfully);
    }
    
    [Fact]
    public async Task SequentionalLoadProfile_SecondCall_ReturnsCompletedTask()
    {
        var profile = new SequentionalLoadProfile();
        _ = profile.WhenNextCanBeExecutedAsync();

        var task = profile.WhenNextCanBeExecutedAsync();
        await Task.Delay(10);
        Assert.False(task.IsCompleted);

        await profile.OnQueryExecutedAsync();
        await Task.Delay(10);
        Assert.True(task.IsCompletedSuccessfully);
    }
}
