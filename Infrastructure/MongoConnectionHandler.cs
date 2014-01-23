namespace Infrastructure
{

	using MongoDB.Driver;
	using MongoDB.Driver.Builders;

	public class MongoConnectionHandler<T> where T : IMongoEntity {
		private readonly MongoServer _server;
		private readonly MongoDatabase _database;
		private readonly MongoCollection<T> _collection;

		public MongoServer Server { get { return _server; } set { value = _server; } }

		public MongoDatabase Database { get { return _database; } set { value = _database; } }

		public MongoCollection<T> MongoCollection { get { return _collection; } private set { value = _collection; } }

		private const string ConnectionString = @"mongodb://localhost";

		public MongoConnectionHandler (string connectionString, string databaseName, CollectionOptionsBuilder options) {
			var client = new MongoClient(connectionString);
			_server = client.GetServer();
			_database = _server.GetDatabase(databaseName);
			var collectionName = typeof(T).Name.ToLower() + "s";
			if ( !_database.CollectionExists(collectionName) && options != null ) {
				_database.CreateCollection(collectionName, options);
			}
			_collection = _database.GetCollection<T>(typeof(T).Name.ToLower() + "s");
		}

		public MongoConnectionHandler (string connectionString, string databaseName) : this(connectionString,databaseName,null) {		
		}

		public MongoConnectionHandler (string databaseName, CollectionOptionsBuilder options) : this(ConnectionString,databaseName,options) {		
		}

		public MongoConnectionHandler (string databaseName) : this(ConnectionString,databaseName) {

		}
	}

	public class MongoConnectionHandler {
		private readonly MongoServer _server;
		private readonly MongoDatabase _database;
		private readonly MongoCollection _collection;

		public MongoServer Server { get { return _server; } set { value = _server; } }

		public MongoDatabase Database { get { return _database; } set { value = _database; } }

		public MongoCollection MongoCollection { get { return _collection; } private set { value = _collection; } }

		private const string ConnectionString = @"mongodb://localhost";

		public MongoConnectionHandler (string connectionString, string databaseName, string collection) {
			var client = new MongoClient(connectionString);
			_server = client.GetServer();
			_database = _server.GetDatabase(databaseName);
			_collection = _database.GetCollection(collection);
		}

		public MongoConnectionHandler (string databaseName, string collection) : this(ConnectionString,databaseName,collection) {

		}
	}
}