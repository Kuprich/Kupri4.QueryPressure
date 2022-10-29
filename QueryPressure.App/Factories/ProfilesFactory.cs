// файлы, в которых будет как-то описываться тест. 
using QueryPressure.App.Arguments;
using QueryPressure.App.Interfaces;
using QueryPressure.Core.Interfaces;

namespace QueryPressure.App.Factories;

public class SettingsFactory<T> where T : ISetting
{
    private readonly string _settingType;
    private IDictionary<string, ICreator<T>> _creators;

    public SettingsFactory(string settingType, IEnumerable<ICreator<T>> creatros)
    {
        _creators = creatros.ToDictionary(x => x.TypeName.ToLower());
        _settingType = settingType;
    }

    public T Create(MainArguments arguments)
    {
        if (!arguments.TryGetValue(_settingType, out var section))
        {
            throw new ApplicationException($"No section {_settingType} was found");
        }

        if (!_creators.TryGetValue(section.Type.ToLower(), out var creator))
        {
            throw new ApplicationException($"No profile with the name of {section.Type}");
        }

        return creator.Create(section);
    }
}
