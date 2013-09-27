
namespace MongoDBDemo
{
	using System;
	using MongoDB.Bson;

	/// <summary>
	/// Description of User.
	/// </summary>
	public class User : MongoEntity
	{
		public string Name { get; set; }
		public int Reputation { get; set; }
	}
}
