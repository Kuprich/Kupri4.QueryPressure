/* 
 * файлы, в которых будет как-то описываться тест, например
 * 
 * connection:
 *      type: Postgres
 *      arguments: 
 *          connectionString: ${POSTGRES_STRING}
 */

namespace QueryPressure.App.Arguments;

public class SectionArguments
{
    public string Type { get; set; } = null!;
    public Dictionary<string, string> Arguments { get; set; } = new();
}
