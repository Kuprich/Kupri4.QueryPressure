// файлы, в которых будет как-то описываться тест. 
using QueryPressure.Arguments;
using QueryPressure.Core.Interfaces;

namespace QueryPressure.Interfaces;

internal interface IProfileCreator
{
    string ProfileTypeName { get; }
    IProfile Create(ProfileArguments profile);
}
