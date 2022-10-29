// файлы, в которых будет как-то описываться тест. 
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

var file = @"
profile:
    type: limitedConcurrency
    arguments: 
        limit: 10
limit: 
    type: queryCount
    arguments: 
        limit: 100
connecition:
    type: Postgres
    connectionString: ${POSTGRES_STRING}   
execution: 
    type: query
    argumens: 
        sql: 'SELECT * FROM sys.allobjects' 
reports: 
    type: csv
    arguments: 
        output: file.csv
";

// далее, все это дело парсится, а запускаться это дело будет сл. образом
//var shell = "querystress bechmark.yml";

file = @"
profile:
    type: limitedConcurrency
    arguments: 
        limit: 10
";

var @params = Deserialize(file);

//var factory = new ProfilesFactory(new[] { new LimitedConcurrencyLoadProfileCreator() });

//var model = factory.CreateProfile(@params);

Console.WriteLine();

MainArguments Deserialize(string fileContent)
{
    var deserializer = new DeserializerBuilder()
        .WithNamingConvention(CamelCaseNamingConvention.Instance)
        .Build();
    return deserializer.Deserialize<MainArguments>(fileContent);
}
