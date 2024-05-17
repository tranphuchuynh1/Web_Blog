using Microsoft.AspNetCore.Mvc;
using Web_Blog.Data;
using Web_Blog.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;

namespace Web_Blog.Controllers
{
    public class CreatePost : Controller
    {
        private readonly WebblogDbContext _db;
        private readonly IWebHostEnvironment _environment;

        public CreatePost(WebblogDbContext db, IWebHostEnvironment environment)
        {
            _db = db;
            _environment = environment;
        }

        [HttpPost]
        public ActionResult Create(string Category, string Title, string Content, IFormFile ImageURL)
        {
            if (ModelState.IsValid)
            {
                DateTime CreatedAt = DateTime.Now;
                string userName = HttpContext.Session.GetString("username");
                int? userId = HttpContext.Session.GetInt32("idUser");

                if (userId == null)
                {
                    return Json(new { success = false, message = "User not logged in." });
                }

                Post post = new Post
                {
                    Category = Category,
                    Title = Title,
                    Content = Content,
                    CreatedAt = CreatedAt,
                    idUser = userId.Value
                };

                if (ImageURL != null && ImageURL.Length > 0)
                {
                    string fileName = Title + Path.GetExtension(ImageURL.FileName);
                    string uploadsFolder = Path.Combine(_environment.WebRootPath, "ProductImages");
                    string filePath = Path.Combine(uploadsFolder, fileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        ImageURL.CopyTo(fileStream);
                    }

                    var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}{HttpContext.Request.PathBase.Value}";
                    post.ImageURL = $"{baseUrl}/ProductImages/{fileName}";
                }

                _db.Posts.Add(post);
                _db.SaveChanges();

                // Điều hướng người dùng đến trang hiển thị bài viết đã đăng
                return Json(new { success = true, imageUrl = post.ImageURL, category = post.Category, title = post.Title, content = post.Content, createdAt = post.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss"), username = userName });
            }
            else
            {
                return Json(new { success = false, message = "Model state is invalid." });
            }
        }
    }
}
