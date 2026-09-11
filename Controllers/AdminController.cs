using Microsoft.AspNetCore.Mvc;

namespace WildTrailsIndia.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}
