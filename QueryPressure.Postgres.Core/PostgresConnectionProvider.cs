using Npgsql;
using QueryPressure.Core.Interfaces;

namespace QueryPressure.Postgres.Core;

public class PostgresConnectionProvider : IConnectionProvider
{
	private readonly string _connectionString;

	public PostgresConnectionProvider(string connectionString)
	{
		_connectionString = connectionString;
	}

	public async Task<IExecutable> CreateExecutorAsync(IScriptSource scriptSource, CancellationToken cancellationToken)
	{
		var script = await scriptSource.GetScriptAsync();

		var connection = new NpgsqlConnection(_connectionString);

		await connection.OpenAsync(cancellationToken);

	return new PostgresExecutor(script, connection);
	}
}
