using QueryPressure.Core.Interfaces;

namespace QueryPressure.Core.ScriptSources;

public class FileScriptSource : IScriptSource
{
    private readonly string _path;

    public FileScriptSource(string path)
    {
        _path = path;
    }

    public Script GetScript()
    {
        if (!File.Exists(_path))
        {
            throw new FileNotFoundException($"File \"{_path}\" not exist!");
        }

        string text = File.ReadAllText(_path);

        return new TextScript(text);
    }
}
