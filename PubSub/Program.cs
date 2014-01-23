using Infrastructure;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PubSub
{
	class MainClass {
		public static void Main (string[] args) {
			Console.WriteLine("Hello World!");
            var topic = "mytopic";
            Task.Factory.StartNew(t => Publish(topic), TaskCreationOptions.None);
            Task.Factory.StartNew(t => Subscribe(topic), TaskCreationOptions.None);			
			Console.ReadLine();
		}

		public static void Subscribe (string topic) {
			var connectionHandler = new MongoConnectionHandler<Message>("MongoDBDemo");
			BsonValue lastId = BsonMinKey.Value;

			while (true) {
				var query = Query.EQ("Topic", topic);
				var cursor = connectionHandler.MongoCollection.FindAs<BsonDocument>(query)
					.SetFlags(QueryFlags.TailableCursor | QueryFlags.AwaitData)
					.SetSortOrder(SortBy.Ascending("$natural"));

				var count = 0;
				foreach (var document in cursor) {
					Console.WriteLine("Message Recieved from topic {0} with data {1}",document["Topic"],document["Data"]);
					//Console.WriteLine(document);
					count++;
				}

				if ( count == 0 ) {
					Thread.Sleep(TimeSpan.FromMilliseconds(1000));
				}
			}  
		}

		internal static IEnumerable<Message> Generator (string topic) {
			for (int i = 0; i < 100; i++) {
				Thread.Sleep(100);
				yield return new Message{ Topic = topic, Data = "Message " + i, Version = 0 };
			}
		}

		public static void Publish (string topic) {
			var options = CollectionOptions.SetCapped(true).SetMaxSize(5000).SetMaxDocuments(100);
			var connectionHandler = new MongoConnectionHandler<Message>("MongoDBDemo", options);
			var messages = Generator(topic);
			foreach (var message in messages) {
				Console.WriteLine("Publishing message");
				connectionHandler.MongoCollection.Insert(message);
			}
		}
	}
}
