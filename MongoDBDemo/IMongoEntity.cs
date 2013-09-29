namespace MongoDBDemo
{
    using MongoDB.Bson;

    public interface IMongoEntity
    {
        ObjectId Id { get; set; }
    }
}