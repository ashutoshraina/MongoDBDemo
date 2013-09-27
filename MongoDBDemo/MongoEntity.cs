
namespace MongoDBDemo
{
	using System;
	using MongoDB.Bson;
	/// <summary>
	/// Description of MongoEntity.
	/// </summary>
	public class MongoEntity : IMongoEntity
	{
		 public ObjectId Id { get; set; }
	}
}
