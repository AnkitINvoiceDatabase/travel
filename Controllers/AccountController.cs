using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WildTrailsIndia.Model;
using WildTrailsIndia.Services.Interfaces;

namespace WildTrailsIndia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;

        public AccountController(
            IUserService userService,
            IAuthService authService)
        {
            _userService = userService;
            _authService = authService;
        }


        // POST: api/Account/Register
        [HttpPost("Register")]
        public async Task<IActionResult> Register(
            [FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Please enter valid information.",
                    errors = ModelState
                        .Where(x => x.Value?.Errors.Count > 0)
                        .ToDictionary(
                            x => x.Key,
                            x => x.Value!.Errors
                                .Select(e => e.ErrorMessage)
                                .ToArray())
                });
            }

            var emailExists =
                await _userService.EmailExistsAsync(model.Email);

            if (emailExists)
            {
                return Conflict(new
                {
                    success = false,
                    message = "Email address already registered."
                });
            }

            var userId =
                await _userService.CreateUserAsync(model);

            // Default Customer Role = 2
            await _userService.AssignRoleAsync(userId, 2);

            return Ok(new
            {
                success = true,
                message = "Registration successful.",
                userId = userId
            });
        }


        // POST: api/Account/Login
        [HttpPost("Login")]
        public async Task<IActionResult> Login(
            [FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Please enter valid information."
                });
            }

            int  result =1 ;
                //await _authService.LoginAsync(model);

           // if (!result.IsSuccess)
            //{
             //   return Unauthorized(result);
            //}

            return Ok(result);
        }


        // POST: api/Account/Logout
        [HttpPost("Logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return Ok(new
            {
                success = true,
                message = "Logout successful."
            });
        }
    }
}
