using QueryPressure.App.Factories;
using QueryPressure.App.Interfaces;
using QueryPressure.App.ProfileCreators;
using QueryPressure.Core.Interfaces;
using QueryPressure.Core.LoadProfiles;

namespace QueryPressure.Tests;


public class LoadProfileFactoryTests
{
    private readonly SettingsFactory<IProfile> _factory;

    public LoadProfileFactoryTests()
    {
        _factory = new SettingsFactory<IProfile>("profile", new ICreator<IProfile>[]
        {
            new SequentialLoadProfileCreator(),
            new SequentialLoadWithDelayProfileCreator(),
            new LimitedConcurrencyLoadProfileCreator(),
            new LimitedConcurrencyWithDelayLoadProfileCreator(),
            new TargetThroughputLoadProfileCreator()
        });
    }

    [Fact]
    public void Create_SequentialLoadProfile_IsCreated()
    {
        var yml = @"
profile:
  type: sequential";
        Assert.IsType<SequentialLoadProfile>(TestUtils.Create(_factory, yml));
    }

    [Fact]
    public void Create_SequentialLoadWithDelayProfile_IsCreated()
    {
        var yml = @"
profile:
  type: sequentialWithDelay
  arguments:
    delay: 00:00:01";

        Assert.IsType<SequentialLoadWithDelayProfile>(TestUtils.Create(_factory, yml));
    }

    [Fact]
    public void Create_LimitedConcurrencyLoadProfile_IsCreated()
    {
        var yml = @"
profile:
  type: LimitedConcurrency
  arguments: 
    limit: 2";
        Assert.IsType<LimitedConcurrencyLoadProfile>(TestUtils.Create(_factory, yml));
    }


    [Fact]
    public void Create_LimitedConcurrencyLoadProfile_ThrowsOnMissedArgument()
    {
        var yml = @"
profile:
  type: limitedConcurrency";

        Assert.Throws<ArgumentException>(() => TestUtils.Create(_factory, yml));
    }

    [Fact]
    public void Create_LimitedConcurrencyLoadProfile_ThrowsOnInvalidArgument()
    {
        var yml = @"
profile:
  type: limitedConcurrency
  arguments: 
    limit: InvalidArgument";

        Assert.Throws<ArgumentException>(() => TestUtils.Create(_factory, yml));
    }

    [Fact]
    public void Create_LimitedConcurrencyWithDelayLoadProfile_IsCreated()
    {
        var yml = @"
profile:
  type: limitedConcurrencyWithDelay
  arguments: 
    limit: 2
    delay: 00:00:01";

        Assert.IsType<LimitedConcurrencyWithDelayLoadProfile>(TestUtils.Create(_factory, yml));
    }

    [Fact]
    public void Create_LimitedConcurrencyWithDelayLoadProfile_ThrowsOnMissedArgument()
    {
        var yml = @"
profile:
  type: limitedConcurrencyWithDelay
  arguments: 
    limit: InvalidArgument";

        Assert.Throws<ArgumentException>(() => TestUtils.Create(_factory, yml));
    }

    [Fact]
    public void Create_TargetThroughputLoadProfile_IsCreated()
    {
        var yml = @"
profile:
  type: targetThroughput
  arguments:
    rps: 50";

        Assert.IsType<TargetThroughputLoadProfile>(TestUtils.Create(_factory, yml));
    }

}