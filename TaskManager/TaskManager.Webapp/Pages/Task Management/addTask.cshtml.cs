using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TaskManager.Application.Repository;
using TaskManager.Application.Domain;
using Task = TaskManager.Application.Domain.Task;

namespace TaskManager.Webapp.Pages;

public class addTaskModel : PageModel
{
    private readonly ILogger<addTaskModel> _logger;
    private readonly TaskRepository _taskRepository;
    private readonly SubjectRepository _subjectRepository;

    public IList<Subject> Subjects { get; set; }

    [BindProperty]
    public TaskInputModel TaskInput { get; set; }

    public class TaskInputModel {
        [Required(ErrorMessage = "Task Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please select a subject")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "Start Date is required")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End Date is required")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "Difficulty level is required")]
        public string Difficulty { get; set; }
    }


    public addTaskModel(TaskRepository taskRepository, SubjectRepository subjectRepository, ILogger<addTaskModel> logger) {
        _taskRepository = taskRepository;
        _subjectRepository = subjectRepository;
        _logger = logger;
    }

    public void OnGet() {
        try {
            string userIdString = HttpContext.Session.GetString("User_Id");
            if (Guid.TryParse(userIdString, out Guid userId)) {
                Subjects = _subjectRepository.GetSubjectsByUserId(userId);
                _logger.LogInformation("Loaded {Count} subjects for user ID {UserId}.", Subjects.Count, userId);
            } else {
                _logger.LogWarning("User ID is invalid or not found in session.");
                Subjects = new List<Subject>();
            }
        } catch (Exception ex) {
            _logger.LogError(ex, "An error occurred while loading subjects.");
            Subjects = new List<Subject>();
        }
    }

    public IActionResult OnPost() {
        if (!ModelState.IsValid) {
            return Page();
        }
        try {
            string userIdString = HttpContext.Session.GetString("User_Id");
            if (Guid.TryParse(userIdString, out Guid userId))
            {
                Task task = new Task
                {
                    Name = TaskInput.Name,
                    Subject = TaskInput.Subject,
                    StartDate = TaskInput.StartDate,
                    EndDate = TaskInput.EndDate,
                    Difficulty = TaskInput.Difficulty,
                    Userid = userId
                }; 
                TempData["SuccessMessage"] = "Task erfolgreich hinzugefügt!";
            _taskRepository.CreateTask(task);
            return RedirectToPage("Task");
            
            } else  {
                ModelState.AddModelError("", "You must be logged in to create a subject.");
                return Page();
            }
            
        } catch (Exception ex) {
            ModelState.AddModelError("", "An error occurred while creating the task.");
            _logger.LogError(ex, "Failed to create task.");
            return Page();
        }
    }
}
