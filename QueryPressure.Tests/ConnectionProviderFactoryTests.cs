using QueryPressure.App.Factories;
using QueryPressure.App.Interfaces;
using QueryPressure.Core.LoadProfiles;

namespace QueryPressure.Tests;

public class ConnectionProviderFactoryTests
{
	public ConnectionProviderFactoryTests()
	{
		_factory = new SettingsFactory<IConnectionProvider>("connection", new ICreator<IConnectionProvider>[]
		{

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

        Assert.IsType<TargetThroughputLoadProfile>(CreateConnection(yml));
    }
}
