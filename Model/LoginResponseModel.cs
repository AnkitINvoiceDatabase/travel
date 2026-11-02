using System.ComponentModel.DataAnnotations;

namespace WildTrailsIndia.Model
{
    public class LoginResponseModel
    {
        public bool IsSuccess { get; set; }

        public string Message { get; set; } = string.Empty;

        public long UserId { get; set; }

        public string FullName { get; set; } = string.Empty;

        public string RoleName { get; set; } = string.Empty;

        public string? RedirectUrl { get; set; }
    }
}
