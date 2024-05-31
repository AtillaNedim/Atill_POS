using MongoDB.Driver;
using Task = TaskManager.Application.Domain.Task;
using TaskManager.Application.Domain;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace TaskManager.Application.Repository
{
    public class TaskRepository
    {
        private readonly ILogger<TaskRepository> _logger;
        private readonly IMongoCollection<Task> _taskCollection;

        public TaskRepository(IMongoCollection<Task> taskCollection)
        {
            _taskCollection = taskCollection;
        }

        public void CreateTask(Task task)
        {
            _taskCollection.InsertOne(task);
        }

        /**
                public List<Task> GetTasksByUserId(Guid userId) {
                    var filter = Builders<Task>.Filter.Eq("UserId", userId);
                    return _taskCollection.Find(filter).ToList();
                }
        */
        public IList<Task> GetAllTasksyUserId(Guid userId)
        {
            try
            {
                return _taskCollection.Find(task => task.Userid == userId).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to load Tasks for user ID {UserId}", userId);
                return new List<Task>();
            }
        }

        public Task GetTaskByGuid(Guid guid) {
            try {
                return _taskCollection.Find<Task>(task => task._id == guid).FirstOrDefault();
            }catch (Exception ex) {
                _logger.LogError(ex, "Failed to load Task with GUID {Guid}", guid);
                return null;
            }
        }
        
        public bool Delete(Guid taskId) {
            try {
                var result = _taskCollection.DeleteOne(task => task._id == taskId);
                _logger.LogInformation("Attempt to delete task with GUID {Guid}, result count: {Count}", taskId, result.DeletedCount);

                if (result.DeletedCount > 0) {
                    _logger.LogInformation("Task with GUID {Guid} was deleted successfully.", taskId);
                    return true;
                } else {
                    _logger.LogWarning("No task found with GUID {Guid} to delete.", taskId);
                    return false;
                }
            } catch (Exception ex) {
                _logger.LogError(ex, "Failed to delete Task with GUID {Guid}", taskId);
                return false;
            }
        }
        
        public long CountTasksByName(string taskName) {
    
            try {
                var filter = Builders<Task>.Filter.Eq("Name", taskName);
                long count = _taskCollection.CountDocuments(filter);
                
                Console.WriteLine(count);
                
                return count;
                
            } catch (Exception ex) {
                _logger.LogError(ex, "Failed to count Tasks with name {TaskName}", taskName);
                return 0;
            }
        }
        
        public long CountTasksBySpecificSubject(string subjectName) {
            try {
                var filter = Builders<Task>.Filter.Eq("Subject", subjectName);
                long count = _taskCollection.CountDocuments(filter);
                return count;
                
            } catch (Exception ex) {
                return 0;
            }
        }
        
        public List<Task> GetTasksBySubject(string subjectName) {
            try {
                return _taskCollection.Find(task => task.Subject == subjectName).ToList();
            } catch (Exception ex) {
                _logger.LogError(ex, "Failed to load Tasks for subject {SubjectName}", subjectName);
                return new List<Task>();
            }
        }

        public async Task<bool> DeleteTaskbid(Guid subjectId) {
            try {
                var deleteResult = await _taskCollection.DeleteOneAsync(x => x._id == subjectId);
                return deleteResult.DeletedCount > 0;
            } catch (Exception ex) {
                _logger.LogError(ex, "Failed to delete subject with ID {SubjectId}", subjectId);
                return false;
            }
        }
    }
}