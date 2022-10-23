using QueryPressure.Arguments;
using QueryPressure.Core.Interfaces;
using QueryPressure.Core.Limits;
using QueryPressure.Extensions;
using QueryPressure.Interfaces;

namespace QueryPressure.LimitCreators;

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
