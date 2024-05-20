using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Web_Blog.Data;
using Web_Blog.Models;
using Microsoft.AspNetCore.Http;
using System.Security.Cryptography;
using System.Text;
using Web_Blog.Models.Interfaces;
using Web_Blog.Models.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using NuGet.Protocol.Core.Types;
namespace Web_Blog.Controllers
{


    public class AccountController : Controller
    {

        private readonly WebblogDbContext _db;
        IcreatepostRepository IcreatepostRepository;

        public AccountController(WebblogDbContext db, IcreatepostRepository createpostRepository)
        {
            _db = db;
            IcreatepostRepository = createpostRepository;
        }



        public static string GetMD5(string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2"));
                }
                return sb.ToString();
            }
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var post = _db.Posts.FirstOrDefault(p => p.PostID == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Post updatedPost)
        {
            if (ModelState.IsValid)
            {
                var existingPost = _db.Posts.FirstOrDefault(p => p.PostID == updatedPost.PostID);
                if (existingPost == null)
                {
                    return NotFound();
                }

                // Cập nhật thông tin bài viết từ updatedPost
                existingPost.Category = updatedPost.Category;
                existingPost.Title = updatedPost.Title;
                existingPost.Content = updatedPost.Content;
                existingPost.ImageBase64 = updatedPost.ImageBase64;
                existingPost.ImageURL = updatedPost.ImageURL;

                _db.SaveChanges();

                return RedirectToAction("Index");
            }

            // Nếu ModelState không hợp lệ, quay lại view chỉnh sửa với dữ liệu hiện tại
            return View(updatedPost);
        }

        public ActionResult DeleteConfirmed(int id)
        {
            var currentUserId = HttpContext.Session.GetInt32("idUser");
            Post post = _db.Posts.Find(id);
            if (post == null)
            {
                return NotFound();
            }
            if (post.idUser != currentUserId)
            {
                return Forbid(); // Nếu không phải, trả về lỗi 403 Forbidden
            }

            _db.Posts.Remove(post);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Index()
        {
            var posts = IcreatepostRepository.getallpost();


            if (HttpContext.Session.GetInt32("idUser") != null)
            {

                string userName = HttpContext.Session.GetString("username");

                ViewBag.UserName = userName;
                return View(posts);
            }
            else
            {
                return RedirectToAction("Login");
            }

        }
        public ActionResult Register()
        {
            return View();
        }

        //POST: Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User _user)
        {
            if (ModelState.IsValid)
            {
                var check = _db.Users.FirstOrDefault(s => s.Email == _user.Email);
                if (check == null)
                {
                    _user.Password = GetMD5(_user.Password);
                    _db.ChangeTracker.AutoDetectChangesEnabled = false;
                    _db.Users.Add(_user);
                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.error = "Email already exists";
                    return View();
                }


            }
            return View();


        }
        public ActionResult Login()
        {

            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string email, string password)
        {

            if (ModelState.IsValid)
            {
                var f_password = GetMD5(password);
                var data = _db.Users.FirstOrDefault(s => s.Email.Equals(email) && s.Password.Equals(f_password));
                if (data != null)
                {
                    // Thêm thông tin vào Session
                    HttpContext.Session.SetString("username", data.username);
                    HttpContext.Session.SetString("Email", data.Email);
                    HttpContext.Session.SetInt32("idUser", data.idUser);
                    HttpContext.Session.SetString("Avatar", data.Avatar);

                    return RedirectToAction("Index");

                }
                else
                {
                    ViewBag.error = "Login failed. Please check your email and password.";
                    return View();
                }


            }




            return View();

        }

        public ActionResult Logout()
        {
            HttpContext.Session.Clear();//remove session
            return RedirectToAction("Login");
        }

    }


}
