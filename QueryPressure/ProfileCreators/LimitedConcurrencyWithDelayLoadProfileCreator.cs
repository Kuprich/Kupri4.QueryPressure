
using QueryPressure.Arguments;
using QueryPressure.Core.Interfaces;
using QueryPressure.Core.LoadProfiles;
using QueryPressure.Extensions;
using QueryPressure.Interfaces;

namespace QueryPressure.ProfileCreators;

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
