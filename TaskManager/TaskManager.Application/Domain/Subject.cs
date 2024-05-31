namespace TaskManager.Application.Domain {

    public class Subject {
        public Guid _id { get; private set; }
        public string Name { get; set; }
        public Guid Userid { get; set; }
        public long TaskCount { get; set; }
        
        public Subject() {
        }
        
        public Subject(string name, Guid usid) {
            _id = Guid.NewGuid();
            Name = name;
            Userid = usid;
        }
    }
}