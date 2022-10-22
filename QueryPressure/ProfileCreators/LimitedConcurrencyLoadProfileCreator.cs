
using QueryPressure.Arguments;
using QueryPressure.Core.Interfaces;
using QueryPressure.Core.LoadProfiles;
using QueryPressure.Extensions;
using QueryPressure.Interfaces;

namespace QueryPressure.ProfileCreators;

// файлы, в которых будет как-то описываться тест. 
public class LimitedConcurrencyLoadProfileCreator : ICreator<IProfile>
{
    public string TypeName => "LimitedConcurrency";
    public IProfile Create(SectionArguments section)
    {
        return new LimitedConcurrencyLoadProfile(
            section.ExtractIntArgumentOrThrow("limit")
        );
    }
}
