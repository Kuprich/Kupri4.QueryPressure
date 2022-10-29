// файлы, в которых будет как-то описываться тест. 
using QueryPressure.App.Arguments;

namespace QueryPressure.App.Interfaces;

public interface ICreator<T>
{
    string TypeName { get; }
    T Create(SectionArguments section);
}
