using QueryPressure.App.Arguments;
using QueryPressure.App.Extensions;
using QueryPressure.App.Interfaces;
using QueryPressure.Core.Interfaces;
using QueryPressure.Core.Limits;

namespace QueryPressure.App.LimitCreators;

public class TimeLimitCreator : ICreator<ILimit>
{
    public string TypeName => "timeLimit";

    public ILimit Create(SectionArguments section)
    {
        return new TimeLimit(
            section.ExtractTimeSpanArgumentOrThrow("limit")
        ); ;
    }
}
