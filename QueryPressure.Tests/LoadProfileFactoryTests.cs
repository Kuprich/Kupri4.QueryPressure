using QueryPressure.Arguments;
using QueryPressure.Core.Interfaces;
using QueryPressure.Factories;
using QueryPressure.Interfaces;
using QueryPressure.ProfileCreators;
using YamlDotNet.Serialization.NamingConventions;
using YamlDotNet.Serialization;
using QueryPressure.LoadProfiles;
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

    private IProfile CreateProfile(string yml)
    {
        var args = Deserialize(yml);
        return _factory.Create(args);
    }

    private MainArguments Deserialize(string fileContent)
    {
        var deserializer = new DeserializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .Build();
        return deserializer.Deserialize<MainArguments>(fileContent);
    }

    [Fact]
    public void Create_SequentialLoadProfile_IsCreated()
    {
        var yml = @"
profile:
  type: sequential";
        Assert.IsType<SequentialLoadProfile>(CreateProfile(yml));
    }

    [Fact]
    public void Create_SequentialLoadWithDelayProfile_IsCreated()
    {
        var yml = @"
profile:
  type: sequentialWithDelay
  arguments:
    delay: 00:00:01";

        Assert.IsType<SequentialLoadWithDelayProfile>(CreateProfile(yml));
    }

    [Fact]
    public void Create_LimitedConcurrencyLoadProfile_IsCreated()
    {
        var yml = @"
profile:
  type: LimitedConcurrency
  arguments: 
    limit: 2";
        Assert.IsType<LimitedConcurrencyLoadProfile>(CreateProfile(yml));
    }


    [Fact]
    public void Create_LimitedConcurrencyLoadProfile_ThrowsOnMissedArgument()
    {
        var yml = @"
profile:
  type: limitedConcurrency";

        Assert.Throws<ArgumentException>(() => CreateProfile(yml));
    }

    [Fact]
    public void Create_LimitedConcurrencyLoadProfile_ThrowsOnInvalidArgument()
    {
        var yml = @"
profile:
  type: limitedConcurrency
  arguments: 
    limit: InvalidArgument";

        Assert.Throws<ArgumentException>(() => CreateProfile(yml));
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

        Assert.IsType<LimitedConcurrencyWithDelayLoadProfile>(CreateProfile(yml));
    }

    [Fact]
    public void Create_LimitedConcurrencyWithDelayLoadProfile_ThrowsOnMissedArgument()
    {
        var yml = @"
profile:
  type: limitedConcurrencyWithDelay
  arguments: 
    limit: InvalidArgument";

        Assert.Throws<ArgumentException>(() => CreateProfile(yml));
    }

    [Fact]
    public void Create_TargetThroughputLoadProfile_IsCreated()
    {
        var yml = @"
profile:
  type: targetThroughput
  arguments:
    rps: 50";

        Assert.IsType<TargetThroughputLoadProfile>(CreateProfile(yml));
    }

}