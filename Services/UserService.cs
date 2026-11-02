using WildTrailsIndia.Data;
using WildTrailsIndia.Helpers;
using WildTrailsIndia.Model;
using WildTrailsIndia.Services.Interfaces;
using Dapper;
namespace WildTrailsIndia.Services
{
        public class UserService : IUserService
        {
            private readonly IDbConnectionFactory _connectionFactory;
            private readonly PasswordHelper _passwordHelper;

            public UserService(
                IDbConnectionFactory connectionFactory,
                PasswordHelper passwordHelper)
            {
                _connectionFactory = connectionFactory;
                _passwordHelper = passwordHelper;
            }

            public async Task<UserModel?> GetUserByEmailAsync(string email)
            {
                using var connection =
                    _connectionFactory.CreateConnection();

                var parameters = new
                {
                    Email = email
                };

                return await connection.QueryFirstOrDefaultAsync<UserModel>(
                    "USP_User_GetByEmail",
                    parameters,
                    commandType:
                        System.Data.CommandType.StoredProcedure);
            }

            public async Task<bool> EmailExistsAsync(string email)
            {
                using var connection =
                    _connectionFactory.CreateConnection();

                var result = await connection.ExecuteScalarAsync<int>(
                    "USP_User_EmailExists",
                    new { Email = email },
                    commandType:
                        System.Data.CommandType.StoredProcedure);

                return result > 0;
            }

            public async Task<long> CreateUserAsync(RegisterModel model)
            {
                using var connection =
                    _connectionFactory.CreateConnection();

                var passwordHash =
                    _passwordHelper.HashPassword(model.Password);

                var parameters = new
                {
                    FullName = model.FullName,
                    Email = model.Email,
                    Mobile = model.Mobile,
                    PasswordHash = passwordHash
                };

                var userId = await connection.ExecuteScalarAsync<long>(
                    "USP_User_Insert",
                    parameters,
                    commandType:
                        System.Data.CommandType.StoredProcedure);

                return userId;
            }

            public async Task<RoleModel?> GetUserRoleAsync(long userId)
            {
                using var connection =
                    _connectionFactory.CreateConnection();

                return await connection.QueryFirstOrDefaultAsync<RoleModel>(
                    "USP_User_GetRole",
                    new { UserId = userId },
                    commandType:
                        System.Data.CommandType.StoredProcedure);
            }

            public async Task<bool> AssignRoleAsync(
                long userId,
                int roleId)
            {
                using var connection =
                    _connectionFactory.CreateConnection();

                var result = await connection.ExecuteAsync(
                    "USP_User_AssignRole",
                    new
                    {
                        UserId = userId,
                        RoleId = roleId
                    },
                    commandType:
                        System.Data.CommandType.StoredProcedure);

                return result > 0;
            }

            public async Task UpdateLastLoginAsync(long userId)
            {
                using var connection =
                    _connectionFactory.CreateConnection();

                await connection.ExecuteAsync(
                    "USP_User_UpdateLastLogin",
                    new { UserId = userId },
                    commandType:
                        System.Data.CommandType.StoredProcedure);
            }

            public async Task IncrementFailedLoginAsync(long userId)
            {
                using var connection =
                    _connectionFactory.CreateConnection();

                await connection.ExecuteAsync(
                    "USP_User_IncrementFailedLogin",
                    new { UserId = userId },
                    commandType:
                        System.Data.CommandType.StoredProcedure);
            }

            public async Task ResetFailedLoginAsync(long userId)
            {
                using var connection =
                    _connectionFactory.CreateConnection();

                await connection.ExecuteAsync(
                    "USP_User_ResetFailedLogin",
                    new { UserId = userId },
                    commandType:
                        System.Data.CommandType.StoredProcedure);
            }
        }
    

}
