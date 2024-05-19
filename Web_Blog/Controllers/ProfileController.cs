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
        private readonly IcreatepostRepository _createpostRepository;
        public ProfileController(WebblogDbContext db, IcreatepostRepository createpostRepository)
        {
            _db = db;
            _createpostRepository = createpostRepository;
        }
        public ActionResult PageProfile()
        {
            int? userId = HttpContext.Session.GetInt32("idUser");
            string userName = HttpContext.Session.GetString("username");
            string email = HttpContext.Session.GetString("Email");
            string iduser = HttpContext.Session.GetString("idUser");
            ViewBag.UserName = userName;
            ViewBag.EMAIL = email;
            ViewBag.Iduser = iduser;
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
        
            return View();
        }
      


    }
}
