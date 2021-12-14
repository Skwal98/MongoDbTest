using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

var conventionPack = new ConventionPack { new IgnoreExtraElementsConvention(true) };
ConventionRegistry.Register("IgnoreExtraElements", conventionPack, type => true);
var conventionCamelCasePack = new ConventionPack { new CamelCaseElementNameConvention() };
ConventionRegistry.Register("camelCase", conventionCamelCasePack, t => true);

var connectionString = "mongodb://localhost:27017";
var databaseName = "EnglishDB";

var client = new MongoClient(connectionString);
var database = client.GetDatabase(databaseName);


//await GetCollectionsNames(client);

var userCol = database.GetCollection<Person>("users");

//userCol.InsertOne(new Person { Name = "vanya2", KKK = 222 });

var filter = new BsonDocument();
using var cursor = await userCol.FindAsync(filter);
while (await cursor.MoveNextAsync())
{
    var people = cursor.Current;
    foreach (var doc in people)
    {
        Console.WriteLine(doc.Name);
    }
}

Console.ReadLine();

static async Task GetDatabaseNames(MongoClient client)
{
    using (var cursor = await client.ListDatabasesAsync())
    {
        var databaseDocuments = await cursor.ToListAsync();
        foreach (var databaseDocument in databaseDocuments)
        {
            Console.WriteLine(databaseDocument["name"]);
        }
    }
}

static async Task GetCollectionsNames(MongoClient client)
{
    using (var cursor = await client.ListDatabasesAsync())
    {
        var dbs = await cursor.ToListAsync();
        foreach (var db in dbs)
        {
            Console.WriteLine("Collection in db {0}:", db["name"]);

            IMongoDatabase database = client.GetDatabase(db["name"].ToString());

            using (var collCursor = await database.ListCollectionsAsync())
            {
                var colls = await collCursor.ToListAsync();
                foreach (var col in colls)
                {
                    Console.WriteLine(col["name"]);
                }
            }
            Console.WriteLine();
        }
    }
}

public class Person
{
    public string Name { get; set; }
}