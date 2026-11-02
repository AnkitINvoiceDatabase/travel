using WildTrailsIndia.Helpers;
using WildTrailsIndia.Model;
using WildTrailsIndia.Services.Interfaces;


namespace WildTrailsIndia.Services
{
   



        public class AuthService : IAuthService
        {
            private readonly IUserService _userService;
            private readonly PasswordHelper _passwordHelper;

            public AuthService(
                IUserService userService,
                PasswordHelper passwordHelper)
            {
                _userService = userService;
                _passwordHelper = passwordHelper;
            }

            public async Task<LoginResponseModel> LoginAsync(
                LoginModel model)
            {
                var response = new LoginResponseModel
                {
                    IsSuccess = false
                };

                var user =
                    await _userService.GetUserByEmailAsync(model.Email);

                if (user == null)
                {
                    response.Message = "Invalid email or password.";
                    return response;
                }

                if (!user.IsActive)
                {
                    response.Message = "Your account is inactive.";
                    return response;
                }

                // Check account lock
                if (user.LockoutEndDate.HasValue &&
                    user.LockoutEndDate.Value > DateTime.UtcNow)
                {
                    response.Message =
                        "Your account is temporarily locked. Please try again later.";

                    return response;
                }

                // Verify password
                var passwordValid =
                    _passwordHelper.VerifyPassword(
                        user.PasswordHash,
                        model.Password);

                if (!passwordValid)
                {
                    await _userService.IncrementFailedLoginAsync(
                        user.UserId);

                    response.Message =
                        "Invalid email or password.";

                    return response;
                }

                // Get user role
                var role =
                    await _userService.GetUserRoleAsync(
                        user.UserId);

                if (role == null)
                {
                    response.Message =
                        "User role is not configured.";

                    return response;
                }

                // Reset failed login count
                await _userService.ResetFailedLoginAsync(
                    user.UserId);

                // Update last login
                await _userService.UpdateLastLoginAsync(
                    user.UserId);

                response.IsSuccess = true;
                response.Message = "Login successful.";
                response.UserId = user.UserId;
                response.FullName = user.FullName;
                response.RoleName = role.RoleName;

                response.RedirectUrl =
                    role.RoleName switch
                    {
                        "Admin" => "/Admin/Dashboard",
                        "TravelAgent" => "/TravelAgent/Dashboard",
                        "Customer" => "/Customer/Dashboard",
                        _ => "/"
                    };

                return response;
            }
        }
    

}
