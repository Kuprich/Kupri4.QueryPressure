namespace QueryPressure.Core.ScriptSources;

public class TextScript : Script
{
    private readonly string _text;

    public TextScript(string text)
    {
        _text = text;
    }
}
