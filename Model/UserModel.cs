namespace WildTrailsIndia.Model
{
    public class UserModel
    {
        public long UserId { get; set; }

        public string FullName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string? Mobile { get; set; }

        public string PasswordHash { get; set; } = string.Empty;

        public bool IsActive { get; set; }

        public bool IsEmailVerified { get; set; }

        public int FailedLoginCount { get; set; }

        public DateTime? LockoutEndDate { get; set; }

        public DateTime? LastLoginDate { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }
    }
}
