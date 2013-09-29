
namespace MongoDBDemo
{
	using System;
	using MongoDB.Bson;

	public class User : MongoEntity
	{
		public string Name { get; set; }
		public int Reputation { get; set; }
	}
}
