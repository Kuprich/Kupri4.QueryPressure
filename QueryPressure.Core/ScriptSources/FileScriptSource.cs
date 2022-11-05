using QueryPressure.Core.Interfaces;

namespace QueryPressure.Core.ScriptSources;

public class FileScriptSource : IScriptSource
{
    private readonly string _path;

    public FileScriptSource(string path)
    {
        _path = path;
    }

    public async Task<Script> GetScriptAsync(CancellationToken cancellationToken = default)
    {
        if (!File.Exists(_path))
        {
            throw new FileNotFoundException($"File \"{_path}\" not exist!");
        }

        string text = await File.ReadAllTextAsync(_path, cancellationToken);

        return new TextScript(text);
    }

}
