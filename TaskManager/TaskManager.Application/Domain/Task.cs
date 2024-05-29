using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager.Application.Domain {
    public class Task {
        public Guid _id { get; set; }
        
        public string Name { get; set; }
        public string Subject { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
        public string Difficulty { get; set; }
        public Guid Userid { get; set; }

        public Task() { }

        public Task(string name, string subject, DateTime startDate, DateTime endDate, string difficulty, Guid usid) {
            _id = Guid.NewGuid();
            Name = name;
            Subject = subject;
            StartDate = startDate;
            EndDate = endDate;
            Difficulty = difficulty;
            Userid = usid;
        }
    }
}