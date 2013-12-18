namespace Infrastructure
{
    using MongoDB.Driver;

    public class MongoConnectionHandler<T> where T : IMongoEntity
    {
        public MongoCollection<T> MongoCollection { get; private set; }
        private const string ConnectionString = @"mongodb://localhost";

        public MongoConnectionHandler(string databaseName)
        {
            var client = new MongoClient(ConnectionString);
            var server = client.GetServer();
            var database = server.GetDatabase(databaseName);
            MongoCollection = database.GetCollection<T>(typeof (T).Name.ToLower() + "s");
        }
    }
}