using System;
using System.ComponentModel.DataAnnotations;

namespace TaskManager.Application.ViewModel
{
    public class TaskViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Subject is required")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Expiration Date is required")]
        public DateTime ExpirationDate { get; set; }

        [Required(ErrorMessage = "Max Points is required")]
        public int MaxPoints { get; set; }

        [Required(ErrorMessage = "Teacher is required")]
        public Guid TeacherId { get; set; }
    }
}