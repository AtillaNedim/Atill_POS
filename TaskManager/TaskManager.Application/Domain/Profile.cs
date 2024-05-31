using System;
using System.ComponentModel.DataAnnotations;
using Bogus.DataSets;

namespace TaskManager.Application.Domain {
    public class Profile {
        [Key]
        public Guid _id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Vorname { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Nachname { get; set; }

        [Required]
        public DateTime Geburtsdatum { get; set; }
        
        public Profile(string vorname, string nachname, DateTime geburtsdatum) {
            _id = Guid.NewGuid();
            Vorname = vorname;
            Nachname = nachname;
            Geburtsdatum = geburtsdatum;
        }

    }
}