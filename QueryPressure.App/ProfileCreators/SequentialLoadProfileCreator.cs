using QueryPressure.App.Arguments;
using QueryPressure.App.Interfaces;
using QueryPressure.Core.Interfaces;
using QueryPressure.Core.LoadProfiles;

namespace QueryPressure.App.ProfileCreators;

public class SequentialLoadProfileCreator : ICreator<IProfile>
{
    public string TypeName => "Sequential";

    public IProfile Create(SectionArguments section)
    {
        return new SequentialLoadProfile();
    }
}
