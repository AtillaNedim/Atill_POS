using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using TaskManager.Application.Domain;
using TaskManager.Application.Repository;
using System.ComponentModel.DataAnnotations;
using System;

namespace TaskManager.Webapp.Pages {
    public class ProfileModel : PageModel {
        private readonly UserRepository _userRepository;

        [BindProperty] public User User { get; set; }

        public ProfileModel(UserRepository userRepository) {
            _userRepository = userRepository;
        }

        public void OnGet() {
            string userIdString = HttpContext.Session.GetString("User_Id");
            if (!string.IsNullOrEmpty(userIdString) && Guid.TryParse(userIdString, out Guid userId)) {
                User = _userRepository.GetUserById(userId);
            }
        }
    }
}