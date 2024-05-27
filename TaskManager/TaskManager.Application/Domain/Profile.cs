using System;
using System.ComponentModel.DataAnnotations;
using Bogus.DataSets;

namespace TaskManager.Application.Domain {
    public class Profile {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Vorname { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Nachname { get; set; }

        [Required]
        public DateTime Geburtsdatum { get; set; }
    }
}