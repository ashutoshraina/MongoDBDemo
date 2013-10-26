namespace MongoDBDemo
{
    using MongoDB.Driver.Builders;
    using MongoDB.Driver.Linq;
    using System;
    using System.Linq;
    using MongoDB.Driver;

    public class OperationsQueries
    {
        protected readonly MongoConnectionHandler<Question> QuestionConnectionHandler;

        public OperationsQueries()
        {
            QuestionConnectionHandler = new MongoConnectionHandler<Question>("MongoDBDemo");
            //dropping all indexes for the demo to make sense
            QuestionConnectionHandler.MongoCollection.DropAllIndexes();
        }

        public void ExplainPlanWithLinqBasedQuery()
        {
            var explainPlan = QuestionConnectionHandler.MongoCollection
                                                  .AsQueryable()
                                                  .Where(q => q.Difficulty >= 3).Explain();
            Console.WriteLine("\n Explain plan for Linq based Query");
            Console.WriteLine("\n" + JsonHelper.FormatJson(explainPlan.ToString()));
        }

        public void ExplainPlanWithBsonDocumentBasedQuery()
        {
            var query = Query<Question>.GTE(q => q.Difficulty, 3);
            var explainPlan = QuestionConnectionHandler.MongoCollection.FindAs<Question>(query).Explain();
            Console.WriteLine("\n Explain plan for BsonDocument based query");
            Console.WriteLine("\n" + JsonHelper.FormatJson(explainPlan.ToString()));
        }

        public void CreateIndex()
        {
            QuestionConnectionHandler.MongoCollection.EnsureIndex(
                IndexKeys.Ascending("Difficulty"),
                IndexOptions.SetBackground(true));
            Console.WriteLine("\n" + "Done creating the indexes");
        }

        public void PrintLinqExpression()
        {
            var result = QuestionConnectionHandler.MongoCollection
                                                  .AsQueryable()
                                                  .Where(q => q.Difficulty >= 2 && q.Text.Length > 10)
                                                  .Expression;

            Console.WriteLine("\n" + result);
        }

        public void GetAllCollections()
        {
            var collections = QuestionConnectionHandler.MongoCollection.Database.GetCollectionNames().ToList();
            Console.WriteLine("\nThe following collections are present in the database");
            collections.ForEach(Console.WriteLine);

            var size = QuestionConnectionHandler.MongoCollection.GetTotalDataSize();
            Console.WriteLine("The total datasize for this collection is {0}", size);
        }

        public void PrintSomeStats()
        {
            var stats = QuestionConnectionHandler.MongoCollection.GetStats();
            Console.WriteLine("Namespace : {0}", stats.Namespace);
            Console.WriteLine("DataSize : {0}", stats.DataSize);
            Console.WriteLine("Index Count : {0}", stats.IndexCount);
            foreach (var indexKey in stats.IndexSizes.Keys)
            {
                Console.WriteLine(indexKey);
            }
        }

        public void ShowAllDatabases()
        {
            var client = new MongoClient(@"mongodb://localhost");
            var server = client.GetServer();
            var databases = server.GetDatabaseNames().ToList();
            Console.WriteLine("\nAll the databases in the server");
            databases.ForEach(Console.WriteLine);
        }
    }
}