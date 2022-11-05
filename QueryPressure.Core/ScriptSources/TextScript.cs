namespace QueryPressure.Core.ScriptSources;

public class TextScript : Script
{
    public string Text { get; init; }

    public TextScript(string text)
    {
        Text = text;
    }
}
