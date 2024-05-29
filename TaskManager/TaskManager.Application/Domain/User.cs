// TODO geburtsdatum format anpassen...
// TODO Task adden format anpassen !!
// TODO mit tasks beginnen.... erledigen
// TODO password hash anpassen !!


using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace TaskManager.Application.Domain {
    public class User {
        
        [Key]
        [BsonId] public Guid _id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Profile Profile { get; set; }

        public User() {
        }

        public User(string email, string password, Profile pfId) {
            _id = Guid.NewGuid();
            Email = email;
            Password = password;
            Profile = pfId;
        }
    }
}