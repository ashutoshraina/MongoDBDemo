namespace Infrastructure
{
    using MongoDB.Bson;

    public class MongoEntity : IMongoEntity
    {
        public ObjectId Id { get; set; }
    }
}