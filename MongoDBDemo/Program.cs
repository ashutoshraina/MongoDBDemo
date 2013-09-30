namespace MongoDBDemo
{
    using System;
    using System.Collections.Generic;

    internal class Program
    {
        public static void Main(string[] args)
        {
            //SeedData();
            //ShowSimpleQueries();
            //ShowAdvancedQueries();
            //ShowOpsQueries();
            Console.Write("\n" + "Press any key to continue . . . ");
            Console.ReadKey(true);
        }

        public static void SeedData()
        {
            var queries = new SimpleQueries();

            //Adding some sample questions
            var questions = new List<Question>();
            var question1 = new Question
                            {
                                Text = "Who are you ?", Answer = "I am MongoDB.",
                                CreatedOn = DateTime.Now, Difficulty = 3
                            };
            var question2 = new Question
                            {
                                Text = "What kind of db are you ?", Answer = "I am a document database.",
                                CreatedOn = DateTime.Now, Difficulty = 3
                            };
            var question3 = new Question
                            {
                                Text = "How fast are you ?", Answer = "I am kind of super quick.",
                                CreatedOn = DateTime.Now, Difficulty = 2
                            };
            var question4 = new Question
                            {
                                Text = "Where are we right now?", Answer = "We are in Pune.",
                                CreatedOn = DateTime.Now, Difficulty = 4
                            };
            questions.Add(question1);
            questions.Add(question2);
            questions.Add(question3);
            questions.Add(question4);
            foreach (var question in questions)
            {
                queries.CreateQuestion(question);
            }

            //Adding some sample Users
            var users = new List<User>();
            var user1 = new User {Name = "Ashutosh", Reputation = 100};
            var user2 = new User {Name = "Foo", Reputation = 70};
            var user3 = new User {Name = "Bar", Reputation = 85};
            var user4 = new User {Name = "Baazz", Reputation = 75};
            users.Add(user1);
            users.Add(user2);
            users.Add(user3);
            users.Add(user4);
            foreach (var user in users)
            {
                queries.CreateUser(user);
            }
        }

        public static void ShowSimpleQueries()
        {
            var queries = new SimpleQueries();
            Console.WriteLine();
            queries.GetAllQuestions();
            Console.WriteLine();
            var id = queries.GetOneQuestion();
            queries.DeleteQuestion(id);
            Console.WriteLine();
        }

        public static void ShowAdvancedQueries()
        {
            var advancedQueries = new AdvancedQueries();
            advancedQueries.UserNameStartsWith("Ba");
            advancedQueries.NumberOfQuestionsWithDifficultyLevel(3);
            advancedQueries.UsersWithReputationGreaterThanUsingBsonDocument(3);
            advancedQueries.UsersWithReputaionGreaterThan(70);
            var createdAfter = new DateTime(2013, 09, 27, 15, 45, 56);
            advancedQueries.QuestionsCreatedAfter(createdAfter);
            advancedQueries.ProjectQuestionThatContains("kind");
        }

        public static void ShowOpsQueries()
        {
            var opsQueries = new OperationsQueries();
            opsQueries.ExplainPlanWithLinqBasedQuery();
            opsQueries.ExplainPlanWithBsonDocumentBasedQuery();
            opsQueries.CreateIndex();
            opsQueries.ExplainPlanWithLinqBasedQuery();
            opsQueries.ExplainPlanWithBsonDocumentBasedQuery();
            opsQueries.PrintLinqExpression();
            opsQueries.GetAllCollections();
            opsQueries.PrintSomeStats();
            opsQueries.ShowAllDatabases();
        }
    }
}