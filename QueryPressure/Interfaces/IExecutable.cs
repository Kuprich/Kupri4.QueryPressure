namespace QueryPressure.Interfaces;

public interface IExecutable
{
    Task ExecuteAsync(CancellationToken cancellationToken);
}