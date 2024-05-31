using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using TaskManager.Application.Repository;
using TaskManager.Application.Domain;
using Task = TaskManager.Application.Domain.Task;

namespace TaskManager.Webapp.Pages;
public class SubjectModel : PageModel {
    private readonly ILogger<SubjectModel> _logger;
    private readonly SubjectRepository _subjectRepository;
    private readonly TaskRepository _taskRepository;

    public IList<Subject> Subjects { get; set; }
    public long TaskCount { get; set; }

    public SubjectModel(SubjectRepository subjectRepository, TaskRepository taskRepository, ILogger<SubjectModel> logger) {
        _subjectRepository = subjectRepository;
        _taskRepository = taskRepository;
        _logger = logger;
    }

    public void OnGet() {
        try {
            string userIdString = HttpContext.Session.GetString("User_Id");
            if (Guid.TryParse(userIdString, out Guid userId)) {
                Subjects = _subjectRepository.GetSubjectsByUserId(userId);
                
                foreach (var subject in Subjects) {
                    subject.TaskCount = _taskRepository.CountTasksBySpecificSubject(subject.Name);
                }
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

}