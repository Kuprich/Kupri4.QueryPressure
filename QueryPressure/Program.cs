
using QueryPressure.App.Arguments;
using YamlDotNet.Serialization.NamingConventions;
using YamlDotNet.Serialization;
using QueryPressure.App;
using QueryPressure.App.Factories;
using QueryPressure.App.Interfaces;
using QueryPressure.App.ProfileCreators;
using QueryPressure.Core.Interfaces;
using QueryPressure.App.LimitCreators;
using QueryPressure.Postgres.App;
using QueryPressure.App.ScriptSourceCreators;

var profileFactory = new SettingsFactory<IProfile>("profile", new ICreator<IProfile>[]
        {
            new SequentialLoadProfileCreator(),
            new SequentialLoadWithDelayProfileCreator(),
            new LimitedConcurrencyLoadProfileCreator(),
            new LimitedConcurrencyWithDelayLoadProfileCreator(),
            new TargetThroughputLoadProfileCreator()
        });

var limitFactory = new SettingsFactory<ILimit>("limit", new ICreator<ILimit>[]
        {
            new QueryCountLimitCreator(),
            new TimeLimitCreator()
        });

var connectionProviderFactory = new SettingsFactory<IConnectionProvider>("connection", new ICreator<IConnectionProvider>[]
        {
            new PostgresConnectionProviderCreator()
        });

var scriptSourceFactory = new SettingsFactory<IScriptSource>("script", new ICreator<IScriptSource>[]
       {
            new FileScriptSourceCreator()
       });

var mainArgs = Merge(args);

var builder = new ScenarioBuilder(profileFactory, limitFactory, connectionProviderFactory, scriptSourceFactory);
var executor = await builder.BuildAsync(mainArgs);
await executor.ExecuteAsync();


Console.WriteLine();

MainArguments Merge(string[] args)
{
    var configExtention = new[] { ".yml", ".yaml" };
    var scriptExtension = ".sql";

    var configFiles = args.Where(x => configExtention.Contains(Path.GetExtension(x)));
    var scriptFile = args.Single(x => scriptExtension.Equals(Path.GetExtension(x)));

    MainArguments result = new();
    foreach (var configFile in configFiles)
    {
        var mainArgs = Deserialize(File.ReadAllText(configFile));

        foreach (var mainArg in mainArgs)
        {
            result.Add(mainArg.Key, mainArg.Value);
        }
    }

    result.Add("script", new SectionArguments
    {
        Type = "file",
        Arguments = new()
        {
            ["path"] = scriptFile
        }
    });

    return result;
}

MainArguments Deserialize(string fileContent)
{
    var deserializer = new DeserializerBuilder()
        .WithNamingConvention(CamelCaseNamingConvention.Instance)
        .Build();
    return deserializer.Deserialize<MainArguments>(fileContent);
}