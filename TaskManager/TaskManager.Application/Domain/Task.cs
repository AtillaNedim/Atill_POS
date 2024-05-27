using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager.Application.Domain {
    public class Task {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Subject Subject { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Difficulty { get; set; }

        public Task() { }

        public Task(string name, Subject subject, DateTime startDate, DateTime endDate, string difficulty) {
            Name = name;
            Subject = subject;
            StartDate = startDate;
            EndDate = endDate;
            Difficulty = difficulty;
        }
    }
}