namespace QueryPressure.Core.Interfaces;

public interface ILimit : ISetting
{
    CancellationToken Token { get; }
}