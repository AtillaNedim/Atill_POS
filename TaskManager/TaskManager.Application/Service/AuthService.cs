using Microsoft.AspNetCore.Http;
using TaskManager.Application.Repository;

namespace TaskManager.Application.Service;

public interface IAuthService {
    bool ValidateUser(string email, string password);
    void SignInUser(HttpContext httpContext, Guid userId);
}

public class AuthService : IAuthService {
    private readonly UserRepository _userRepository;

    public AuthService(UserRepository userRepository) {
        _userRepository = userRepository;
    }

    public bool ValidateUser(string email, string password) {
                var user = _userRepository.GetUserByEmail(email);
                if (user != null) {
                    return _userRepository.ValidatePassword(password, user.Password);
                }
                
        return false;
    }

    public void SignInUser(HttpContext httpContext, Guid userId) {
        httpContext.Session.SetString("UserID", userId.ToString());
    }
}
