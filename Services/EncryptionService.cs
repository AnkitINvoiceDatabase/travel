using System.Security.Cryptography;
using System.Text;


namespace WildTrailsIndia.Services
{
    /// <summary>
    /// Simple helper to hash (encrypt) strings using SHA‑256.
    /// In a real app you would use a proper password‑hashing algorithm such as BCrypt,
    /// PBKDF2, or Argon2. This is only for demonstration purposes.
    /// </summary>
    public static class EncryptionService
    {
        public static string Encrypt(string plainText)
        {
            if (plainText == null) return null;
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(plainText);
            var hash = sha256.ComputeHash(bytes);
            // Convert to a hex string
            var sb = new StringBuilder();
            foreach (var b in hash)
                sb.Append(b.ToString("x2"));
            return sb.ToString();
        }
    }
}
