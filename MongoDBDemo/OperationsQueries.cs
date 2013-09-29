﻿using MongoDB.Bson;
namespace MongoDBDemo
{
	using MongoDB.Driver.Builders;
	using MongoDB.Driver.Internal;
	using MongoDB.Driver.Linq;
	using System;
	using System.Linq;
	using MongoDB.Driver;
	
	public class OperationsQueries
	{
		protected readonly MongoConnectionHandler<Question> QuestionConnectionHandler;
		
		public OperationsQueries()
		{
			QuestionConnectionHandler = new MongoConnectionHandler<Question>();
			//dropping all indexes for the demo to make sense
			QuestionConnectionHandler.MongoCollection.DropAllIndexes();
		}
		
		public void ExplainPlan()
		{
			var result = QuestionConnectionHandler.MongoCollection
									.AsQueryable<Question>()
									.Where(q => q.Difficulty >= 3).Explain();
			Console.WriteLine("\n"+result);
		}
		
		public void CreateIndex()
		{
			QuestionConnectionHandler.MongoCollection.EnsureIndex(
							IndexKeys.Ascending("Difficulty"),
							IndexOptions.SetBackground(true));
			Console.WriteLine("\n"+"Done creating the indexes");
		}
		
		public void PrintLinqExpression()
		{
			var result = QuestionConnectionHandler.MongoCollection
									.AsQueryable<Question>()
									.Where(q =>q.Difficulty >= 2 && q.Text.Length > 10)
									.Expression;
			
			Console.WriteLine("\n"+result);
		}
		
		public void GetAllCollections()
		{
			var collections = QuestionConnectionHandler.MongoCollection.Database.GetCollectionNames().ToList();
			Console.WriteLine("\nThe following collections are present in the database");
			collections.ForEach(collection => Console.WriteLine(collection));
			
			var size = QuestionConnectionHandler.MongoCollection.GetTotalDataSize();
			Console.WriteLine("The total datasize for this collection is {0}",size);
		}
		
		public void PrintSomeStats()
		{
			var stats = QuestionConnectionHandler.MongoCollection.GetStats();
			Console.WriteLine("Namespace : {0}",stats.Namespace);
			Console.WriteLine("DataSize : {0}",stats.DataSize);
			Console.WriteLine("Index Count : {0}",stats.IndexCount);
			foreach (var indexKey in stats.IndexSizes.Keys) {
				Console.WriteLine(indexKey);
			}
		}
		
		public void ShowAllDatabases()
		{
			var client = new MongoClient(@"mongodb://localhost");
            var server = client.GetServer();
            var databases = server.GetDatabaseNames().ToList();
            Console.WriteLine("\nAll the databases in the server");
            databases.ForEach(database => Console.WriteLine(database));
		}
	}
}
