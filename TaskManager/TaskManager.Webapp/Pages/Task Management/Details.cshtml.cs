using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskManager.Application.Repository;
using TaskManager.Application.Domain;
using Task = TaskManager.Application.Domain.Task;

namespace TaskManager.Webapp.Pages;

public class DetailsModel : PageModel {
    private readonly TaskRepository _tasks;
    public Task TaskDetails { get; set; }

    public DetailsModel(TaskRepository tasks) {
        _tasks = tasks;
    }
    
    public IActionResult OnGet(Guid guid) {
        TaskDetails = _tasks.GetTaskByGuid(guid);
        
        if (TaskDetails == null) {
            return RedirectToPage("/NotFound");
        }
        
        return Page();
    }
}