
namespace MongoDBDemo
{
	using MongoDB.Driver.Builders;
	using MongoDB.Driver.Internal;
	using MongoDB.Driver.Linq;
	using System;
	using System.Linq;
	
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
				IndexKeys.Ascending("Difficulty"),IndexOptions.SetBackground(true));
			Console.WriteLine("\n"+"Done creating the indexes");
		}
		
		public void PrintLinqExpression()
		{
			var result = QuestionConnectionHandler.MongoCollection
				.AsQueryable<Question>().Where(q =>q.Difficulty >= 2 && q.Text.Length > 10).Expression;
			
			Console.WriteLine("\n"+result);
		}
	}
}
