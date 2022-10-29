using QueryPressure.Core.LoadProfiles;

namespace QueryPressure.Tests.ProfileTests;

public class LimitedConcurrencyLoadProfileTests
{
    [Fact]
    public async Task WhenNextCanBeExecutedAsync_TheNextAfterLimitExceededTaskCompleted_After_OnQueryExecutedAsync_Called()
    {
        var profile = new LimitedConcurrencyLoadProfile(2);

        var task1 = profile.WhenNextCanBeExecutedAsync();
        var task2 = profile.WhenNextCanBeExecutedAsync();
        var task3 = profile.WhenNextCanBeExecutedAsync();

        Assert.True(task1.IsCompletedSuccessfully);
        Assert.True(task2.IsCompletedSuccessfully);

        await Task.Delay(10);
        Assert.False(task3.IsCompleted);

        await profile.OnQueryExecutedAsync();
        await Task.Delay(10);
        Assert.True(task3.IsCompletedSuccessfully);

    }

}