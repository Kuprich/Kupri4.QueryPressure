using QueryPressure.App.Arguments;
using QueryPressure.App.Extensions;
using QueryPressure.App.Interfaces;
using QueryPressure.Core.Interfaces;
using QueryPressure.Core.LoadProfiles;

namespace QueryPressure.App.ProfileCreators;

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
