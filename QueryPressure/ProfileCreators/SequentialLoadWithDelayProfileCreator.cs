
using QueryPressure.Arguments;
using QueryPressure.Core.Interfaces;
using QueryPressure.Core.LoadProfiles;
using QueryPressure.Extensions;
using QueryPressure.Interfaces;

namespace QueryPressure.ProfileCreators;

public class SequentialLoadWithDelayProfileCreator : ICreator<IProfile>
{
    public string TypeName => "SequentialWithDelay";

    public IProfile Create(SectionArguments section)
    {
        return new SequentialLoadWithDelayProfile(
            section.ExtractTimeSpanArgumentOrThrow("delay")
        );
    }
}
