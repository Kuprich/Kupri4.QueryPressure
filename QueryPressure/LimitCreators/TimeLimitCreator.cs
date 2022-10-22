using QueryPressure.Arguments;
using QueryPressure.Core.Interfaces;
using QueryPressure.Core.Limits;
using QueryPressure.Extensions;
using QueryPressure.Interfaces;

namespace QueryPressure.LimitCreators;

public class TimeLimitCreator : ICreator<ILimit>
{
    public string TypeName => "TimeLimit";

    public ILimit Create(SectionArguments section)
    {
        return new TimeLimit(
            section.ExtractTimeSpanArgumentOrThrow("limit")
        ); ;
    }
}
