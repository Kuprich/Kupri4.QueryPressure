namespace QueryPressure.Core.Interfaces;

public interface IScriptSource : ISetting
{
    Task<Script> GetScriptAsync(CancellationToken cancellationToken = default);
}
