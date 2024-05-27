using MongoDB.Driver;
using Task = TaskManager.Application.Domain.Task;
using TaskManager.Application.Domain;
namespace TaskManager.Application.Repository
{
    public class TaskRepository
    {
        private readonly IMongoCollection<Task> _taskCollection;

        public TaskRepository(IMongoCollection<Task> taskCollection) {
            _taskCollection = taskCollection;
        }

        public void CreateTask(Task task) {
            _taskCollection.InsertOne(task);
        }
    }
}