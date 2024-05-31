using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskManager.Application.Repository;
using TaskManager.Application.Domain;

namespace TaskManager.Webapp.Pages {
    public class AddSubject : PageModel {
        private readonly SubjectRepository _subjectRepository;

        public AddSubject(SubjectRepository subjectRepository) {
            _subjectRepository = subjectRepository;
        }

        public async Task<IActionResult> OnPostAsync(string subjectName) {
            if (!ModelState.IsValid) {
                return Page();
            }
            
            string userIdString = HttpContext.Session.GetString("User_Id");
            if (Guid.TryParse(userIdString, out Guid userId)) {
                var subject = new Subject { Name = subjectName, Userid = userId };
                _subjectRepository.CreateSubject(subject);
                return RedirectToPage("/Task Management/addTask");
            } else  {
                ModelState.AddModelError("", "You must be logged in to create a subject.");
                return Page();
            }
        }
    }
}