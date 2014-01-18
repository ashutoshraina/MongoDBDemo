namespace MongoDBDemo.Queries
{
	using System;
	using Infrastructure;
	using MongoDB.Driver;
	using MongoDB.Driver.Builders;
	using MongoDBDemo.Model;

	public class OptimisticConcurrency {
		protected readonly MongoConnectionHandler<Person> PersonConnectionHandler;

		public OptimisticConcurrency () {
			PersonConnectionHandler = new MongoConnectionHandler<Person>("MongoDBDemo");
		}

		public void CreatePerson (Person person) {
			var count = PersonConnectionHandler.MongoCollection.Count();
			if ( count == 1 ) {
				System.Console.WriteLine("Person already exists");
			} else {
				var newPerson = PersonConnectionHandler.MongoCollection.Insert<Person>(person, WriteConcern.Acknowledged);
				if ( newPerson.Ok ) {
					System.Console.WriteLine("A Person was created successfully");
				} else {
					System.Console.WriteLine("There was an error in creating the person" + newPerson.LastErrorMessage);
				}
			}
		}

		public Person GetPerson () {
			var person = PersonConnectionHandler.MongoCollection.FindOneAs<Person>();
			return person;
		}

		public bool EditPerson (Person person) {
			var query = Query.And(Query<Person>.EQ(_ => _.Version, person.Version), Query<Person>.EQ(_ => _.Id, person.Id));
			var updatedPerson = person;
			updatedPerson.Version = person.Version + 1;
			var result = PersonConnectionHandler.MongoCollection.FindAndModify(query, null, Update.Replace<Person>(updatedPerson), true);
			if ( result.ModifiedDocument != null ) {
				Console.WriteLine("Document Modified successfully");
				Console.WriteLine(result.ModifiedDocument);
				return true;
			} else {
				return false;
			}
		}
	}
}
