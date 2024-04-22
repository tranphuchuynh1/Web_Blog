using Microsoft.AspNetCore.Mvc;

namespace Web_Blog.Controllers
{
    public class TourismController : Controller
    {
        public IActionResult PageTourism()
        {
            return View();
        }
    }
}
