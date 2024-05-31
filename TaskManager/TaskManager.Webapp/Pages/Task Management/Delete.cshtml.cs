using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using TaskManager.Application.Repository;

namespace TaskManager.WebApp.Pages.Tasks;

public class DeleteModel : PageModel {
    private readonly ILogger<DeleteModel> _logger;
    private readonly TaskRepository _taskRepository;

    public DeleteModel(TaskRepository taskRepository, ILogger<DeleteModel> logger) {
        _taskRepository = taskRepository;
        _logger = logger;
    }

    public async Task<IActionResult> OnGetAsync(string taskId) {
        if (string.IsNullOrEmpty(taskId)) {
            _logger.LogError("Subject ID is null or empty");
            return NotFound("Subject ID is not specified");
        }

        try {
            var taskGuid = Guid.Parse(taskId);
            bool isDeleted = await _taskRepository.DeleteTaskbid(taskGuid);

            if (!isDeleted) {
                _logger.LogError("No subject found with ID {TaskId}", taskId);
                return NotFound($"No subject found with ID {taskId}");
            }

            TempData["SuccessMessage"] = "Subject deleted successfully.";
            return RedirectToPage("Task");
        } catch (Exception ex) {
            _logger.LogError(ex, "Error deleting subject with ID {TaskId}", taskId);
            return StatusCode(500, "Internal server error");
        }
    }
}