using MongoDB.Driver;
using Task = TaskManager.Application.Domain.Task;
using TaskManager.Application.Domain;
namespace TaskManager.Application.Repository
{
    public class SubjectRepository
    {
        private readonly IMongoCollection<Subject> _subjectCollection;

        public SubjectRepository(IMongoCollection<Subject> subjectCollection) {
            _subjectCollection = subjectCollection;
        }

        public void CreateSubject(Subject subject) {
             _subjectCollection.InsertOneAsync(subject);
        }
    }
}