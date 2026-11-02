using Microsoft.AspNetCore.Mvc;
using WildTrailsIndia.Model;
using WildTrailsIndia.Services;
namespace WildTrailsIndia.Controllers
{
    public class AdminController : Controller
    {

        // GET: /Admin/Dashboard
        public IActionResult Dashboard()
        {
            return View();
        }
        // GET: /Admin/Login
        [HttpGet]
        public IActionResult Login()
        {
            // Render the login view. Add authentication logic as needed.
            // Render the login form.
            return View();
        }
        // POST: /Admin/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            // TODO: replace with real authentication logic
            bool isAuthenticated = false; // placeholder
            // ----- Authentication logic (example) -----
            // Encrypt the incoming password before any comparison or storage.
            // In a production app you would use a proper password‑hashing algorithm
            // (e.g., BCrypt, PBKDF2, Argon2) and compare the hash with the stored one.
            var encryptedPassword = EncryptionService.Encrypt(model.Password);
            // Simulate a credential check against a user store.
            // Replace this stub with a real DbContext lookup:
            // var user = _dbContext.AdminUsers.FirstOrDefault(u => u.Username == model.Username && u.PasswordHash == encryptedPassword);
           // bool isAuthenticated = false; // placeholder – set to true if user is found
            if (isAuthenticated)
            {
                // On success redirect to dashboard
                return RedirectToAction(nameof(Dashboard));
            }
            // If authentication fails, show a generic error.
            ModelState.AddModelError(string.Empty, "Invalid username or password.");
            // Optionally, you could log the failed attempt here.
            return View(model);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Register(string Name, string Email, string Password, string ConfirmPassword)
        //{
        //    // Registration logic yahan aayega

        //    return RedirectToAction("Login");
        //}


        //public IActionResult Login()
        //{
        //    return View();
        //}
        //public IActionResult Dashboard()
        //{
        //    return View();
        //}


        //[HttpGet]
        //public IActionResult Register()
        //{
        //    return View();
        //}
    }
}
