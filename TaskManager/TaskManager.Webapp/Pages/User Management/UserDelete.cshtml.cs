using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using TaskManager.Application.Repository;

namespace TaskManager.WebApp.Pages.Tasks;

public class UserDeleteModel : PageModel {
    private readonly ILogger<UserDeleteModel> _logger;
    private readonly UserRepository _userRepository;
    private readonly ProfileRepository _profileRepository;

    public UserDeleteModel(UserRepository userRepository, ProfileRepository profileRepository, ILogger<UserDeleteModel> logger) {
        _userRepository = userRepository;
        _profileRepository = profileRepository;
        _logger = logger;
    }

    public async Task<IActionResult> OnGetAsync() {
        string userIdString = HttpContext.Session.GetString("User_Id");
        string userProfileIdString = HttpContext.Session.GetString("UserProfile_Id");

        if (Guid.TryParse(userIdString, out Guid userguid)) {
            if (Guid.TryParse(userProfileIdString, out Guid profileguid)) {
                bool isDeleted = await _userRepository.DeleteUserbyid(userguid);
                bool isDeletedProfile = await _profileRepository.DeleteProfilebyid(profileguid);
            
                if (!isDeleted && !isDeletedProfile) {
                    _logger.LogError("No User found with ID {UserId}", userguid);
                    _logger.LogError("No Profile found with ID {ProfileId}", profileguid);

                    return NotFound($"No User found with ID {userguid}");
                }

                return RedirectToPage("Logout");
            } else {
                _logger.LogError("Invalid GUID for profile: {ProfileGUID}", userProfileIdString);
                return StatusCode(500, "Internal server error due to invalid profile GUID.");
            }
        } else {
            _logger.LogError("Invalid GUID for user: {UserGUID}", userIdString);
            return StatusCode(500, "Internal server error due to invalid user GUID.");
        }
    }

}