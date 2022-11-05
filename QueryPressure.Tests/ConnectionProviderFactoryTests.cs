using QueryPressure.App.Factories;
using QueryPressure.App.Interfaces;
using QueryPressure.Core.Interfaces;
using QueryPressure.Core.LoadProfiles;
using QueryPressure.Postgres.App;
using QueryPressure.Postgres.Core;

namespace QueryPressure.Tests;

public class ConnectionProviderFactoryTests
{
	private SettingsFactory<IConnectionProvider> _factory;

	public ConnectionProviderFactoryTests()
	{
		_factory = new SettingsFactory<IConnectionProvider>("connection", new ICreator<IConnectionProvider>[]
		{
			new PostgresConnectionProviderCreator()
		});
	}

	[Fact]
	public void Create_PostgresConnectionProvider_IsCreated()
	{
		var yml = @"
connection:
  type: postgres
  arguments:
    connectionString: ${POSTGRES_STRING}";

		var provider = TestUtils.Create(_factory, yml);


        Assert.IsType<PostgresConnectionProvider>(TestUtils.Create(_factory, yml));
	}
}
