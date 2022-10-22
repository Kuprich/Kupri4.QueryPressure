// файлы, в которых будет как-то описываться тест. 
using QueryPressure.Arguments;
using QueryPressure.Core.Interfaces;

namespace QueryPressure.Interfaces;

public interface ICreator<T>
{
    string TypeName { get; }
    T Create(SectionArguments section);
}
