namespace MongoDBDemo
{
	using System;
	using System.Linq;
	using MongoDB.Driver.Linq;
	using MongoDB.Driver.Builders;
    using MongoDB.Bson;
    
	public class AdvancedQueries
	{
		protected readonly MongoConnectionHandler<User> UserConnectionHandler;
		protected readonly MongoConnectionHandler<Question> QuestionConnectionHandler;
		
		public AdvancedQueries()
		{
			UserConnectionHandler = new MongoConnectionHandler<User>();
			QuestionConnectionHandler = new MongoConnectionHandler<Question>();
		}
		
		public void UsersWithReputaionGreaterThan(int reputation)
		{
			var result = UserConnectionHandler.MongoCollection.AsQueryable<User>()
                                              .Where(u => u.Reputation > reputation).ToList();

			Console.WriteLine("We found {0} Users With Reputation greater than {0}",result.Count(),reputation);

			foreach (var user in result) {
				Console.WriteLine("User : {0} -- Reputation : {1}",user.Name,user.Reputation);
			}			
		}
		
		public void NumberOfQuestionsWithDifficultyLevel(int difficulty)
		{
			var result = QuestionConnectionHandler.MongoCollection.AsQueryable<Question>()
                                                  .Where(q => q.Difficulty >= difficulty);

			Console.WriteLine("We found {0} Questions have a difficulty level equal to {1}",result.Count(),difficulty);
		}
		
		public void QuestionsCreatedAfter(DateTime createdAfter)
		{
			var result = from question in QuestionConnectionHandler.MongoCollection.AsQueryable<Question>() 
						 where question.CreatedOn > createdAfter
						 select question;

			Console.WriteLine("We found {0} Questions created after date {1}",result.Count(),createdAfter);
//			foreach (var question in result) {
//				Console.WriteLine("Question Id : {0}, was created on {1}",question.Text,question.CreatedOn);
//			}
		}
		
		public void UserNameStartsWith(string searchKey)
		{
            var query = Query.Matches("Name",new BsonRegularExpression(string.Format("^{0}",searchKey)));
			var result = UserConnectionHandler.MongoCollection.Find(query).ToList();

            Console.WriteLine("We found {0} Users whose name starts with {1}",result.Count(),searchKey);
		}
		
		public void ProjectQuestionThatContains(string searchText)
		{
            var result = QuestionConnectionHandler.MongoCollection
                                .FindAllAs<Question>()
                                .Where(q => q.Text.Contains(searchText)).ToList();

            Console.WriteLine("We found {0} Questions that contain text {1}",result.Count(),searchText);
		}
	}
}
