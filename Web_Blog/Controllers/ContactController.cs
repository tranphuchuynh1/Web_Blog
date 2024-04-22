using Microsoft.AspNetCore.Mvc;

namespace Web_Blog.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult PageContact()
        {
            return View();
        }
    }
}
