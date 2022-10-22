using QueryPressure.Arguments;
using System.Xml.Linq;

namespace QueryPressure.Extensions;

public static class SectionArgumentsExtension
{
    public static int ExtractIntArgumentOrThrow(this SectionArguments section, string argumentName)
    {
        string val = ExtractStringArgumentOrThrow(section, argumentName);

        if (!int.TryParse(val, out int result))
        {
            throw new ArgumentException($"The value presented as an argument named '{argumentName}' is not a valid integer. The value is '{val}'");
        }

        return result;
    }

    public static TimeSpan ExtractTimeSpanArgumentOrThrow(this SectionArguments section, string argumentName)
    {
        string val = ExtractStringArgumentOrThrow(section, argumentName);

        if (!TimeSpan.TryParse(val, out TimeSpan result))
        {
            throw new ArgumentException($"The value presented as an argument named '{argumentName}' is not a valid integer. The value is '{val}'");
        }

        return result;
    }

    public static string ExtractStringArgumentOrThrow(this SectionArguments section, string argumentName)
    {
        if (string.IsNullOrWhiteSpace(argumentName) || !section.Arguments.TryGetValue(argumentName, out var result))
        {
            throw new ArgumentException($"There is no argument with named '{argumentName}' in {section.Type}");
        }
        return result;
    }
}
