using QueryPressure.App.Arguments;
using QueryPressure.App.Extensions;
using QueryPressure.App.Interfaces;
using QueryPressure.Core.Interfaces;
using QueryPressure.Core.ScriptSources;

namespace QueryPressure.App.ScriptSourceCreators;

public class FileScriptSourceCreator : ICreator<IScriptSource>
{
    public string TypeName => "file";

    public IScriptSource Create(SectionArguments section)
    {
        return new FileScriptSource(
            section.ExtractStringArgumentOrThrow("path"));
    }
}
