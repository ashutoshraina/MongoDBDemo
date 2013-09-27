namespace MongoDBDemo
{
	using System;
	using MongoDB.Driver;
	using System.Linq;
	using MongoDB.Bson;
	using MongoDB.Driver.Builders;
	
	public class SimpleQueries
	{
		protected readonly MongoConnectionHandler<User> UserConnectionHandler;
		protected readonly MongoConnectionHandler<Question> QuestionConnectionHandler;
		
		public SimpleQueries()
		{
			UserConnectionHandler = new MongoConnectionHandler<User>();
			QuestionConnectionHandler = new MongoConnectionHandler<Question>();
		}
		
		public void CreateQuestion(Question question)
		{
			 //// Save the entity with safe mode (WriteConcern.Acknowledged)
            var result = this.QuestionConnectionHandler.MongoCollection.Save<Question>(question, 
			                             new MongoInsertOptions
                                         { WriteConcern = WriteConcern.Acknowledged });
 
            if (!result.Ok){
            	Console.WriteLine(result.LastErrorMessage);
            }
            else if(result.Response["err"] != null){
            	Console.WriteLine("Insertion was successfull");
            }
		}
		
		public void CreateUser(User	user)
		{
			 //// Save the entity with safe mode (WriteConcern.Acknowledged)
            var result = this.UserConnectionHandler.MongoCollection.Save<User>(user, 
			                             new MongoInsertOptions { WriteConcern = WriteConcern.Acknowledged });
 
            if (!result.Ok){
            	Console.WriteLine(result.LastErrorMessage);
            }
            else if(result.Response["err"] != null){
            	Console.WriteLine("Insertion was successfull");
            }
		}
		
		public void GetAllQuestions()
		{
			var cursor = this.QuestionConnectionHandler.MongoCollection.FindAllAs<Question>();
			var resultSet = cursor.ToList();

			Console.WriteLine("Writing out all the questions");
			foreach (var result in resultSet) {
				Console.WriteLine("Text : {0},  Answer : {1}",result.Text,result.Answer);
			}
		}
		
		public ObjectId GetOneQuestion()
		{
			var cursor = this.QuestionConnectionHandler.MongoCollection.FindOneAs<Question>();

            Console.WriteLine(cursor.Id);
			return cursor.Id;
		}
		
		public void DeleteQuestion(ObjectId id)
		{
			 var result = this.QuestionConnectionHandler.MongoCollection.Remove(
							Query<Question>.EQ(e => e.Id, id), RemoveFlags.None, WriteConcern.Acknowledged);
 
            if (!result.Ok){
            	Console.WriteLine(result.ErrorMessage);
            }
            else{
            	Console.WriteLine("Delete Operation OK : {0}",result.Ok);
            }
		}
	}
}
