using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using TaskManager.Application.Repository;

namespace TaskManager.WebApp.Pages;

public class SubjectDeleteModel : PageModel {
    private readonly ILogger<SubjectDeleteModel> _logger;
    private readonly SubjectRepository _subjectRepository;

    public SubjectDeleteModel(SubjectRepository subjectRepository, ILogger<SubjectDeleteModel> logger) {
        _subjectRepository = subjectRepository;
        _logger = logger;
    }

    public async Task<IActionResult> OnGetAsync(string subjectId) {
        if (string.IsNullOrEmpty(subjectId)) {
            _logger.LogError("Subject ID is null or empty");
            return NotFound("Subject ID is not specified");
        }

        try {
            var subjectGuid = Guid.Parse(subjectId);
            bool isDeleted = await _subjectRepository.DeleteSubjectById(subjectGuid);

            if (!isDeleted) {
                _logger.LogError("No subject found with ID {SubjectId}", subjectId);
                return NotFound($"No subject found with ID {subjectId}");
            }

            TempData["SuccessMessage"] = "Subject deleted successfully.";
            return RedirectToPage("Subject");
        } catch (Exception ex) {
            _logger.LogError(ex, "Error deleting subject with ID {SubjectId}", subjectId);
            return StatusCode(500, "Internal server error");
        }
    }
}