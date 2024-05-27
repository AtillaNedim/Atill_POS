using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using TaskManager.Application.Domain;
using TaskManager.Application.Repository;
using System.ComponentModel.DataAnnotations;
using System;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;

namespace TaskManager.Webapp.Pages {
    public class UpdateProfileModel : PageModel {
        private readonly ILogger<UpdateProfileModel> _logger;
        private readonly ProfileRepository _profileRepository;

        [BindProperty]
        public UpdateProfileInputModel Input { get; set; }

        public class UpdateProfileInputModel {
            [Required]
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

        public UpdateProfileModel(ILogger<UpdateProfileModel> logger, ProfileRepository profileRepository) {
            string userIdString = HttpContext.Session.GetString("User_Id");
            _logger = logger;
            _profileRepository = profileRepository;
        }

        public async Task<IActionResult> OnGetAsync(Guid id) {
            var profile = await Task.Run(() => _profileRepository.GetProfileById(id));
            if (profile == null) {
                return NotFound("Profile not found");
            }

            Input = new UpdateProfileInputModel {
                Vorname = profile.Vorname,
                Nachname = profile.Nachname,
                Geburtsdatum = profile.Geburtsdatum
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync() {
            if (!ModelState.IsValid) {
                return Page();
            }

            var profileToUpdate = new Profile {
                Id = Input.Id,
                Vorname = Input.Vorname,
                Nachname = Input.Nachname,
                Geburtsdatum = Input.Geburtsdatum
            };

            try {
                await Task.Run(() => _profileRepository.UpdateProfile(profileToUpdate));
                _logger.LogInformation("Profile updated successfully.");
            } catch (Exception ex) {
                _logger.LogError($"Error updating profile: {ex.Message}");
                ModelState.AddModelError(string.Empty, "An error occurred while updating the profile.");
                return Page();
            }

            return RedirectToPage("./Profile");
        }
    }
}
