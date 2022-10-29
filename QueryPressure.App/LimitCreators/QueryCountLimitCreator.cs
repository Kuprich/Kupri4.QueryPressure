using QueryPressure.App.Arguments;
using QueryPressure.App.Extensions;
using QueryPressure.App.Interfaces;
using QueryPressure.Core.Interfaces;
using QueryPressure.Core.Limits;

namespace QueryPressure.App.LimitCreators;

public class QueryCountLimitCreator : ICreator<ILimit>
{
    public string TypeName => "queryCount";

    public ILimit Create(SectionArguments section)
    {
        return new QueryCountLimit(
            section.ExtractIntArgumentOrThrow("limit")
        );
    }
}
