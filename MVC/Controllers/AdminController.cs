using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        // Trang chính của Admin Dashboard
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}
