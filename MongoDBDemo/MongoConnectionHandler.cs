
namespace MongoDBDemo
{
	using System;
	using MongoDB.Driver;

	/// <summary>
	/// Description of MongoConnectionHandler.
	/// </summary>
	public class MongoConnectionHandler<T> where T : IMongoEntity
	{
		public MongoCollection<T> MongoCollection { get; private set; }
		private const string connectionString = @"mongodb://localhost";
		public MongoConnectionHandler()
		{
			var client = new MongoClient(connectionString);
            var server = client.GetServer();
            var database = server.GetDatabase("MongoDBDemo");
            MongoCollection = database.GetCollection<T>(typeof(T).Name.ToLower() + "s");
		}
	}
}
