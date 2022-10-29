using QueryPressure.App.Arguments;
using QueryPressure.App.Extensions;
using QueryPressure.App.Interfaces;
using QueryPressure.Core.Interfaces;
using QueryPressure.Core.LoadProfiles;

namespace QueryPressure.App.ProfileCreators;

public class TargetThroughputLoadProfileCreator : ICreator<IProfile>
{
    public string TypeName => "targetThroughput";

    public IProfile Create(SectionArguments section)
    {
        return new TargetThroughputLoadProfile(
            section.ExtractIntArgumentOrThrow("rps")
        );
    }
}
