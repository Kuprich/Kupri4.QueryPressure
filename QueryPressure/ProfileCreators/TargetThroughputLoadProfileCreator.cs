
using QueryPressure.Arguments;
using QueryPressure.Core.Interfaces;
using QueryPressure.Core.LoadProfiles;
using QueryPressure.Extensions;
using QueryPressure.Interfaces;

namespace QueryPressure.ProfileCreators;

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
