﻿namespace MongoDBDemo
{
    using System;
    using Infrastructure;
    public class Question : MongoEntity
    {
        public string Text { get; set; }
        public string Answer { get; set; }
        public DateTime CreatedOn { get; set; }
        public int Difficulty { get; set; }
    }
}