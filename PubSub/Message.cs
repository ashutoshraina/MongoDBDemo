namespace PubSub
{
    using Infrastructure;
    using System;

    public class Message : MongoEntity
    {
        public string Topic { get; set; }

        public String Data { get; set; }

        public int Version { get; set; }
    }
}
