namespace QueryPressure.Interfaces;

public interface IProfile
{
    Task<bool> WhenNextCanBeExecuted(CancellationToken cancellationToken);
}
