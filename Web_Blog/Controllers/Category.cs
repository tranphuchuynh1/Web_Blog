using Microsoft.AspNetCore.Mvc;
using Web_Blog.Data;
using Web_Blog.Models;
using Web_Blog.Models.Interfaces;

namespace Web_Blog.Controllers
{
    public class Category : Controller
    {
        private readonly WebblogDbContext _db;
        public Category(WebblogDbContext db)
        {
            _db = db;

        }

        public IActionResult InLiving()
        {
            int? userId = HttpContext.Session.GetInt32("idUser");
            var user = _db.Users.Find(userId);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            var posts = _db.Posts.Where(p => p.Category == "option1").ToList();

            ViewBag.username = user.username;
            ViewBag.userAvatar = user.Avatar;

            return View(posts);
        }
        public IActionResult Tourism()
        {
            int? userId = HttpContext.Session.GetInt32("idUser");
            var user = _db.Users.Find(userId);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            var posts = _db.Posts.Where(p => p.Category == "option2" ).ToList();

            ViewBag.username = user.username;
            ViewBag.userAvatar = user.Avatar;

            return View(posts);
        }
        public IActionResult Technology()
        {
            int? userId = HttpContext.Session.GetInt32("idUser");
            var user = _db.Users.Find(userId);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            var posts = _db.Posts.Where(p => p.Category == "option3").ToList();

            ViewBag.username = user.username;
            ViewBag.userAvatar = user.Avatar;

            return View(posts);
        }
        public IActionResult Fashion()
        {
            int? userId = HttpContext.Session.GetInt32("idUser");
            var user = _db.Users.Find(userId);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            var posts = _db.Posts.Where(p => p.Category == "option4").ToList();

            ViewBag.username = user.username;
            ViewBag.userAvatar = user.Avatar;

            return View(posts);
        }

    }
}
