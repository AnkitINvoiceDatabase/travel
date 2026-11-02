using WildTrailsIndia.Model;

namespace WildTrailsIndia.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserModel?> GetUserByEmailAsync(string email);

        Task<long> CreateUserAsync(RegisterModel model);

        Task<bool> EmailExistsAsync(string email);

        Task<RoleModel?> GetUserRoleAsync(long userId);

        Task<bool> AssignRoleAsync(long userId, int roleId);

        Task UpdateLastLoginAsync(long userId);

        Task IncrementFailedLoginAsync(long userId);

        Task ResetFailedLoginAsync(long userId);
    }
}
