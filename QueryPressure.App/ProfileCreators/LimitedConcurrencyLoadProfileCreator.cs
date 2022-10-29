using QueryPressure.App.Arguments;
using QueryPressure.App.Extensions;
using QueryPressure.App.Interfaces;
using QueryPressure.Core.Interfaces;
using QueryPressure.Core.LoadProfiles;

namespace QueryPressure.App.ProfileCreators;

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
