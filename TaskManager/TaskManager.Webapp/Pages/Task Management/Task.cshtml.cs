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
public class TaskModel : PageModel {
    private readonly ILogger<TaskModel> _logger;
    private readonly TaskRepository _taskRepository;
    private readonly SubjectRepository _subjectRepository;

    public IList<Task> Tasks { get; set; }

    public TaskModel(TaskRepository taskRepository, SubjectRepository subjectRepository, ILogger<TaskModel> logger) {
        _taskRepository = taskRepository;
        _subjectRepository = subjectRepository;
        _logger = logger;
    }

    public void OnGet() {
        try {
            string userIdString = HttpContext.Session.GetString("User_Id");
            if (Guid.TryParse(userIdString, out Guid userId)) {
                Tasks = _taskRepository.GetAllTasksyUserId(userId);
                _logger.LogInformation("Loaded {Count} subjects and tasks for user ID {UserId}.",Tasks.Count, userId);
            } else {
                _logger.LogWarning("User ID is invalid or not found in session.");
                Tasks = new List<Task>();
            }
        } catch (Exception ex) {
            _logger.LogError(ex, "An error occurred while loading subjects or tasks.");
            Tasks = new List<Task>();
        }
    }
}
