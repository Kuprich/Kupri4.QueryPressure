
using QueryPressure.Arguments;
using QueryPressure.Core.Interfaces;
using QueryPressure.Interfaces;
using QueryPressure.LoadProfiles;

namespace QueryPressure.ProfileCreators;

public class SequentialLoadProfileCreator : ICreator<IProfile>
{
    public string TypeName => "Sequential";

    public IProfile Create(SectionArguments section)
    {
        return new SequentialLoadProfile();
    }
}
