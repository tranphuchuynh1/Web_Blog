using Microsoft.AspNetCore.Mvc;

namespace Web_Blog.Controllers
{
    public class FashionController : Controller
    {
        public IActionResult PageFashion()
        {
            return View();
        }
    }
}
