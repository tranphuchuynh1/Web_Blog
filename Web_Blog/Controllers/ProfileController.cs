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
            string userName = HttpContext.Session.GetString("username");
            string email = HttpContext.Session.GetString("Email");

            // Gán tên người dùng vào ViewBag để truyền sang view
            ViewBag.UserName = userName;
            ViewBag.EMAIL = email;
            return View();
        }



    }
}
