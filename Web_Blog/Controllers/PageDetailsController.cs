using Microsoft.AspNetCore.Mvc;

namespace Web_Blog.Controllers
{
    public class PageDetailsController : Controller
    {
        public IActionResult PageDetails()
        {
            return View();
        }
    }
}
