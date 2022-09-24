// файлы, в которых будет как-то описываться тест. 
using QueryPressure.Core.Interfaces;
using QueryPressure.Interfaces;
using QueryPressure.Arguments;

namespace QueryPressure.Factories;

internal class LoadProfilesFactory
{
    private IDictionary<string, IProfileCreator> _creators;

    public LoadProfilesFactory(IEnumerable<IProfileCreator> creatros)
    {
        _creators = creatros.ToDictionary(x => x.ProfileTypeName);
    }

    public IProfile CreateProfile(MainArguments arguments)
    {
        var profile = arguments.Profile;

        if (!_creators.TryGetValue(profile.Type, out var creator))
        {
            throw new ApplicationException($"No profile with the name of {profile.Type}");
        }

        return creator.Create(profile);
    }
}
