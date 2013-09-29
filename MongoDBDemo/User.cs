namespace MongoDBDemo
{
    public class User : MongoEntity
    {
        public string Name { get; set; }
        public int Reputation { get; set; }
    }
}