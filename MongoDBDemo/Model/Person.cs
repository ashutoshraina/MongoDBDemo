namespace MongoDBDemo.Model
{
    using Infrastructure;
using System;

    public class Person : MongoEntity
    {
        private readonly long _version = DateTime.UtcNow.Ticks;
        public string Name { get; set; }
        public long Version { get { return _version; } private set { value = _version; } }
    }
}
