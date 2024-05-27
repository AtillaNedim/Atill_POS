using Bogus;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using TaskManager.Application.Domain;
using Task = TaskManager.Application.Domain.Task;

namespace TaskManager.Application.Infrastructure {
    public class TasksContext {
        private readonly IMongoDatabase _database;

        public TasksContext(string connectionString, string databaseName) {
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(databaseName);
        }

        public IMongoCollection<User> Users => _database.GetCollection<User>("users");
        public IMongoCollection<Task> Tasks => _database.GetCollection<Task>("tasks");
        public IMongoCollection<Subject> Subjects => _database.GetCollection<Subject>("subjects");
        public IMongoCollection<Profile> Profiles => _database.GetCollection<Profile>("profiles");
    }

}

