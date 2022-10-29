using QueryPressure.App.Arguments;
using QueryPressure.App.Extensions;
using QueryPressure.App.Interfaces;
using QueryPressure.Core.Interfaces;
using QueryPressure.Postgres.Core;

namespace QueryPressure.Postgres.App;

public class PostgresConnectionProviderCreator : ICreator<IConnectionProvider>
{
    public string TypeName => "postgres";

    public IConnectionProvider Create(SectionArguments section)
    {
        string connectionString = section.ExtractStringArgumentOrThrow("connectionString");

        return new PostgresConnectionProvider(connectionString);
    }
}