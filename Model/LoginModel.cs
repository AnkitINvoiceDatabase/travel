using System.ComponentModel.DataAnnotations;

namespace WildTrailsIndia.Model
{
    public class LoginModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        [Required]
        public string CaptchaToken { get; set; } = string.Empty;

        [Required]
        public string CaptchaCode { get; set; } = string.Empty;
    }
}
