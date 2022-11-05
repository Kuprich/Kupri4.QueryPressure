using Npgsql;
using QueryPressure.Core;
using QueryPressure.Core.Interfaces;
using QueryPressure.Core.ScriptSources;

namespace QueryPressure.Postgres.Core;

internal class PostgresExecutor : IExecutable
{
	private readonly TextScript _script;
	private readonly NpgsqlConnection _connection;

	public PostgresExecutor(Script script, NpgsqlConnection connection)
	{
		if (script is not TextScript textScript)
		{
			throw new NotImplementedException("The only supported script type is TextScript");
		}
		_script = textScript;
		_connection = connection;
	}

	public void Dispose()
	{
		_connection.Dispose();
	}

	public async Task ExecuteAsync(CancellationToken cancellationToken = default)
	{
		await using var cmd = _connection.CreateCommand();
		cmd.CommandText = _script.Text;
		var reader = await cmd.ExecuteReaderAsync(cancellationToken);
		await reader.ReadAsync(cancellationToken);
	}
}