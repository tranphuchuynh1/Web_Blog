using Microsoft.AspNetCore.Mvc;
using Web_Blog.Data;
using Web_Blog.Models;
using Web_Blog.Models.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Linq;
namespace Web_Blog.Controllers
{
    public class ProfileController : Controller
    {
        private readonly WebblogDbContext _db;
        private readonly IWebHostEnvironment _environment;

        public ProfileController(WebblogDbContext db, IWebHostEnvironment environment)
        {
            _db = db;
            _environment = environment;
        }

        public IActionResult PageProfile()
        {
            int? userId = HttpContext.Session.GetInt32("idUser");
            string userName = HttpContext.Session.GetString("username");
            string email = HttpContext.Session.GetString("Email");
            string iduser = HttpContext.Session.GetString("idUser");
            var user = _db.Users.Find(userId);
            ViewBag.UserName = userName;
            ViewBag.EMAIL = email;
            ViewBag.Iduser = iduser;
            ViewBag.Avatar = user.Avatar;
            if (userId == null)
            {
                return RedirectToAction("Login");
            }

            var userPosts = _db.Posts
                               .Where(p => p.idUser == userId)
                               .OrderByDescending(p => p.CreatedAt)
                               .ToList();

            ViewBag.UserPosts = userPosts;
            // Gán tên người dùng vào ViewBag để truyền sang view


            return View(user);
        }

        [HttpPost]
        public ActionResult UploadAvatar(IFormFile avatar)
        {
            int? userId = HttpContext.Session.GetInt32("idUser");
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var user = _db.Users.Find(userId);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            if (avatar != null && avatar.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(avatar.FileName);
                var uploadsFolderPath = Path.Combine(_environment.WebRootPath, "ProductImages");

                // Create directory if it doesn't exist
                if (!Directory.Exists(uploadsFolderPath))
                {
                    Directory.CreateDirectory(uploadsFolderPath);
                }

                var filePath = Path.Combine(uploadsFolderPath, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    avatar.CopyTo(fileStream);
                }

                user.Avatar = "/ProductImages/" + fileName;
                _db.SaveChanges();
            }

            return RedirectToAction("PageProfile");
        }


    }
}
