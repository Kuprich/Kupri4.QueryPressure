using QueryPressure.App.Arguments;
using QueryPressure.Core.Interfaces;
using YamlDotNet.Serialization.NamingConventions;
using YamlDotNet.Serialization;
using QueryPressure.App.Factories;

namespace QueryPressure.Tests;

public static  class TestUtils
{
    public static T Create<T>(SettingsFactory<T> factory, string yml) where T : ISetting
    {
        var args = Deserialize(yml);
        return factory.Create(args);
    }

    public static MainArguments Deserialize(string fileContent)
    {
        var deserializer = new DeserializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .Build();
        return deserializer.Deserialize<MainArguments>(fileContent);
    }
}
