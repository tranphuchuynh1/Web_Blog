using Microsoft.AspNetCore.Mvc;

namespace Web_Blog.Controllers
{
    public class TechnologyController : Controller
    {
        public IActionResult PageTechnology()
        {
            return View();
        }
    }
}
