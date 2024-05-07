using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_Blog.Data;
using Web_Blog.Models;          

namespace Web_Blog.Controllers
{
    public class ProfileController : Controller
    {
        public IActionResult PageProfile()
        {
            return View();
        }



    }
}
