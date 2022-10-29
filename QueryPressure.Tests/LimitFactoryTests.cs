using QueryPressure.App.Arguments;
using QueryPressure.App.Factories;
using QueryPressure.App.Interfaces;
using QueryPressure.App.LimitCreators;
using QueryPressure.Core.Interfaces;
using QueryPressure.Core.Limits;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace QueryPressure.Tests;

public class LimitFactoryTests
{
    private readonly SettingsFactory<ILimit> _factory;
    public LimitFactoryTests()
    {
        _factory = new SettingsFactory<ILimit>("limit", new ICreator<ILimit>[]
        {
            new QueryCountLimitCreator(),
            new TimeLimitCreator()
        });
    }

    [Fact]
    public void Create_QueryCountLimit_IsCreated()
    {
        var yml = @"
limit:
  type: queryCount
  arguments: 
    limit: 100";
        Assert.IsType<QueryCountLimit>(TestUtils.Create(_factory, yml));
    }

    [Fact]
    public void Create_TimeLimit_IsCreated()
    {
        var yml = @"
limit:
  type: timeLimit
  arguments: 
    limit: 00:00:01";
        Assert.IsType<TimeLimit>(TestUtils.Create(_factory, yml));
    }
}
