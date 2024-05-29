using MongoDB.Driver;
using Task = TaskManager.Application.Domain.Task;
using TaskManager.Application.Domain;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace TaskManager.Application.Repository
{
    public class TaskRepository {
        private readonly ILogger<TaskRepository> _logger;
        private readonly IMongoCollection<Task> _taskCollection;

        public TaskRepository(IMongoCollection<Task> taskCollection) {
            _taskCollection = taskCollection;
        }

        public void CreateTask(Task task) {
            _taskCollection.InsertOne(task);
        }
/** 
        public List<Task> GetTasksByUserId(Guid userId) {
            var filter = Builders<Task>.Filter.Eq("UserId", userId);
            return _taskCollection.Find(filter).ToList();
        }
*/
        public IList<Task> GetAllTasksyUserId(Guid userId) {
            try {
                return _taskCollection.Find(task => task.Userid == userId).ToList();
            } catch (Exception ex) {
                _logger.LogError(ex, "Failed to load Tasks for user ID {UserId}", userId);
                return new List<Task>();
            }
        }
    }
}