﻿namespace MongoDBDemo
{
	using System;
    using MongoDB.Bson;
 
    public interface IMongoEntity
    {
        ObjectId Id { get; set; }
    }
}
