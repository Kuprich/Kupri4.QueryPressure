using QueryPressure.App.Arguments;
using QueryPressure.App.Extensions;
using QueryPressure.App.Interfaces;
using QueryPressure.Core.Interfaces;
using QueryPressure.Core.LoadProfiles;

namespace QueryPressure.App.ProfileCreators;

public class LimitedConcurrencyWithDelayLoadProfileCreator : ICreator<IProfile>
{
    public string TypeName => "limitedConcurrencyWithDelay";

    public IProfile Create(SectionArguments section)
    {
        return new LimitedConcurrencyWithDelayLoadProfile(
            section.ExtractIntArgumentOrThrow("limit"),
            section.ExtractTimeSpanArgumentOrThrow("delay")
        );
    }
}
