// файлы, в которых будет как-то описываться тест. 
using QueryPressure.Core.Interfaces;
using QueryPressure.Interfaces;
using QueryPressure.Arguments;

namespace QueryPressure.Factories;

public class SettingsFactory<T> where T : ISetting
{
    private IDictionary<string, ICreator<T>> _creators;

    public SettingsFactory(IEnumerable<ICreator<T>> creatros)
    {
        _creators = creatros.ToDictionary(x => x.TypeName.ToLower());
    }

    public T CreateProfile(MainArguments arguments)
    {
        var profile = arguments.Profile;

        if (!_creators.TryGetValue(profile.Type.ToLower(), out var creator))
        {
            throw new ApplicationException($"No profile with the name of {profile.Type}");
        }

        return creator.Create(profile);
    }
}
