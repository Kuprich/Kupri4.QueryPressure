// файлы, в которых будет как-то описываться тест. 

namespace QueryPressure.Arguments;

public class ProfileArguments
{
    public string Type { get; set; } = null!;
    public Dictionary<string, string> Arguments { get; set; } = new();
}
