using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using TaskManager.Application.Domain;
using TaskManager.Application.Repository;
using System.ComponentModel.DataAnnotations;
using System;

namespace TaskManager.Webapp.Pages {
    public class RegisterModel : PageModel {
        private readonly ILogger<RegisterModel> _logger;
        private readonly UserRepository _userRepository;
        private readonly ProfileRepository _profileRepository;

        [BindProperty]
        public string Email { get; set; }
        
        [BindProperty, DataType(DataType.Password)]
        public string Password { get; set; }
        
        [BindProperty]
        public string vorname { get; set; }
        
        [BindProperty]
        public string nachname { get; set; }
        
        [BindProperty, DataType(DataType.Date)]
        public DateTime geburtsdatum { get; set; }

        public RegisterModel(ILogger<RegisterModel> logger, UserRepository userRepository, ProfileRepository profileRepository) {
            _logger = logger;
            _userRepository = userRepository;
            _profileRepository = profileRepository;
        }

        public void OnGet() {
        }

        public IActionResult OnPost() {
            if (!ModelState.IsValid) {
                _logger.LogError("Registration form is not valid.");
                return Page();
            }

            if (_userRepository.DoesUserExistByEmail(Email)) {
                _logger.LogError($"User with email {Email} already exists.");
                ModelState.AddModelError("Email", "An account with this email already exists.");
                return Page();
            }

            Profile profile = new Profile(vorname, nachname, geburtsdatum);
            _profileRepository.CreateProfile(profile);

            string hashedPassword = _userRepository.HashPassword(Password);

            User newUser = new User(Email, hashedPassword, profile);
            _userRepository.CreateUser(newUser);

            _logger.LogInformation($"New user registered: {Email}");
            _logger.LogInformation($"New Profile for {vorname} registered with ID: {profile._id}");
            _logger.LogInformation($"User ID: {newUser._id}");

            return RedirectToPage("Login");
        }

    }
}
