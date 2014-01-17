namespace MongoDBDemo.Queries
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading;
	using System.Threading.Tasks;
	using Infrastructure;
	using MongoDB.Bson;
	using MongoDB.Bson.IO;
	using MongoDB.Driver;
	using MongoDB.Driver.Builders;

	public class QueryOpLog {
		protected readonly MongoConnectionHandler OpLogHandler;

		public QueryOpLog () {
			OpLogHandler = new MongoConnectionHandler("local", "oplog.rs");
		}

		public IEnumerable<BsonDocument> GetLastEntryInOpLog () {           
			BsonValue lastId = BsonMinKey.Value;

			while (true) {
                var query = Query.GT("ts", lastId);
				var cursor = OpLogHandler.MongoCollection.FindAs<BsonDocument>(query)
                            .SetFlags(QueryFlags.TailableCursor | QueryFlags.AwaitData)
                            .SetSortOrder(SortBy.Ascending("$natural"));

				var count = 0;
				foreach (var document in cursor) {
					lastId = document["ts"];					
					yield return document;
					count++;
				}

				if ( count == 0 ) {
					Thread.Sleep(TimeSpan.FromMilliseconds(100));
				}
			}           
		}
	}
}
