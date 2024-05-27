using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskManager.Application.Repository;
using TaskManager.Application.Domain;
using Task = TaskManager.Application.Domain.Task;

namespace TaskManager.Webapp.Pages;

public class TaskModel : PageModel {
    private readonly TaskRepository _taskRepository;
    private readonly SubjectRepository _subjectRepository;

    [BindProperty]
    public Task Task { get; set; } 

    [BindProperty]
    public string NewSubject { get; set; }

    public TaskModel(TaskRepository taskRepository, SubjectRepository subjectRepository) {
        _taskRepository = taskRepository;
        _subjectRepository = subjectRepository;
    }

    public async Task<IActionResult> OnPostAsync(string action) {
        if (!ModelState.IsValid) {
            return Page();
        }
    
        if (action == "AddSubject" && !string.IsNullOrEmpty(NewSubject)) {
            string userId = HttpContext.Session.GetString("User_Id"); 
            var subject = new Subject { Name = NewSubject, Userid = new Guid(userId)};
            _subjectRepository.CreateSubject(subject);
            return RedirectToPage();
        }
    
        _taskRepository.CreateTask(Task);
        return RedirectToPage("./Index");
    }

}