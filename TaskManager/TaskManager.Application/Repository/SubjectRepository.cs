using MongoDB.Driver;
using TaskManager.Application.Domain;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Task = System.Threading.Tasks.Task;
namespace TaskManager.Application.Repository
{
    public class SubjectRepository {
        private readonly ILogger<SubjectRepository> _logger;
        private readonly IMongoCollection<Subject> _subjectCollection;

        public SubjectRepository(IMongoCollection<Subject> subjectCollection)
        {
            _subjectCollection = subjectCollection;
        }

        public void CreateSubject(Subject subject) {
            _subjectCollection.InsertOne(subject);
        }

        public IList<Subject> GetAllSubjects() {
            return _subjectCollection.Find(_ => true).ToList();
        }
        

        public IList<Subject> GetSubjectsByUserId(Guid userId) {
            try {
                return _subjectCollection.Find(subject => subject.Userid == userId).ToList();
            } catch (Exception ex) {
                _logger.LogError(ex, "Failed to load subjects for user ID {UserId}", userId);
                return new List<Subject>();
            }
        }

    }
}
