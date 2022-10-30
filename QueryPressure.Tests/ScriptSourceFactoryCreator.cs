using QueryPressure.App.Arguments;
using QueryPressure.App.Factories;
using QueryPressure.App.Interfaces;
using QueryPressure.App.ScriptSourceCreators;
using QueryPressure.Core.Interfaces;
using QueryPressure.Core.LoadProfiles;
using QueryPressure.Core.ScriptSources;

namespace QueryPressure.Tests;

public class ScriptSourceFactoryCreator
{
    private readonly SettingsFactory<IScriptSource> _factory;
    public ScriptSourceFactoryCreator()
    {
        _factory = new SettingsFactory<IScriptSource>("script", new ICreator<IScriptSource>[]
        {
            new FileScriptSourceCreator()
        });
    }

    [Fact]
    public void Create_FileScriptSource_IsCreated()
    {
        var yml = @"
script:
  type: file
  arguments:
    path: scriptPath.sql";
        Assert.IsType<FileScriptSource>(TestUtils.Create(_factory, yml));
    }

    [Fact]
    public void Create_FileScriptSource_IsCreated_Alternative()
    {

        var section = new SectionArguments
        {
            Type = "file",
            Arguments = new()
            {
                ["path"] = "scriptPath.sql"
            }
        };

        Assert.IsType<FileScriptSource>(_factory.Create(new MainArguments {
            ["script"] = section
        }));

    }
}
