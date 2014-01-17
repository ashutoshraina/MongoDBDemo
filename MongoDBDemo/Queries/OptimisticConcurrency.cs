namespace MongoDBDemo.Queries
{
    using Infrastructure;
    using MongoDB.Driver;
    using MongoDB.Driver.Builders;
    using MongoDBDemo.Model;

    public class OptimisticConcurrency
    {
        protected readonly MongoConnectionHandler<Person> PersonConnectionHandler;

        public OptimisticConcurrency()
        {
            PersonConnectionHandler = new MongoConnectionHandler<Person>("MongoDBDemo");
            PersonConnectionHandler.MongoCollection.Drop();
        }

        public void CreatePerson(Person person)
        {
            var result = PersonConnectionHandler.MongoCollection.Insert<Person>(person, WriteConcern.Acknowledged);
            if (result.Ok)
            {
                System.Console.WriteLine("Person was created successfully");
            }
            else
            {
                System.Console.WriteLine("There was an error in creating the person" + result.LastErrorMessage);
            }
 
        }

        public bool EditPerson(Person person)
        {
            var query =  Query.And(Query<Person>.EQ(_ => _.Version,person.Version) ,Query<Person>.EQ(_=> _.Id,person.Id));
            var result = PersonConnectionHandler.MongoCollection.FindAndModify(query,null,Update.Replace<Person>(person),true);
            return result.ModifiedDocument != null;
        }
    }
}
