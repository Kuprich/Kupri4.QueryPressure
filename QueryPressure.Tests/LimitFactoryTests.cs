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

    private ILimit CreateLimit(string yml)
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
    public void Create_QueryCountLimit_IsCreated()
    {
        var yml = @"
limit:
  type: queryCount
  arguments: 
    limit: 100";
        Assert.IsType<QueryCountLimit>(CreateLimit(yml));
    }

    [Fact]
    public void Create_TimeLimit_IsCreated()
    {
        var yml = @"
limit:
  type: timeLimit
  arguments: 
    limit: 00:00:01";
        Assert.IsType<TimeLimit>(CreateLimit(yml));
    }
}
