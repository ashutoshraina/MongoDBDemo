namespace MongoDBDemo.Model
{
	using Infrastructure;
	using System;

	public class Person : MongoEntity {
		public string Name { get; set; }

		public long Version { get ; set ; }
	}
}
