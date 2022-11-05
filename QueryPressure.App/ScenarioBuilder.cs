using QueryPressure.App.Arguments;
using QueryPressure.App.Factories;
using QueryPressure.Core;
using QueryPressure.Core.Interfaces;

namespace QueryPressure.App;

public class ScenarioBuilder
{
	private readonly SettingsFactory<IProfile> _profileFactory;
	private readonly SettingsFactory<ILimit> _limitFactory;
	private readonly SettingsFactory<IConnectionProvider> _connectionProviderFactory;
	private readonly SettingsFactory<IScriptSource> _scriptSourceFactory;

	public ScenarioBuilder(
		SettingsFactory<IProfile> profileFactory,
		SettingsFactory<ILimit> limitFactory,
		SettingsFactory<IConnectionProvider> connectionProviderFactory,
		SettingsFactory<IScriptSource> scriptSourceFactory)
	{
		_profileFactory = profileFactory;
		_limitFactory = limitFactory;
		_connectionProviderFactory = connectionProviderFactory;
		_scriptSourceFactory = scriptSourceFactory;
	}

	public async Task<QueryExecutor> BuildAsync(MainArguments arguments, CancellationToken cancellationToken = default)
	{
		var profile = _profileFactory.Create(arguments);
		var limit = _limitFactory.Create(arguments);
		var connectionProvider = _connectionProviderFactory.Create(arguments);
		var scriptSource = _scriptSourceFactory.Create(arguments);

		var executor = await connectionProvider.CreateExecutorAsync(scriptSource, cancellationToken);

		return new QueryExecutor(executor, profile, limit);
	}
}
