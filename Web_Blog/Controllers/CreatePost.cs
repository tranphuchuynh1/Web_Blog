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
                    idUser = userId.Value,
                    PostedBy = userName 
                };


                if (ImageURL != null && ImageURL.Length >0)
               {
                    // Mã hóa ảnh thành chuỗi Base64
                    using (var memoryStream = new MemoryStream())
                    {
                        ImageURL.CopyTo(memoryStream);
                        byte[] imageBytes = memoryStream.ToArray();
                        post.ImageBase64 = Convert.ToBase64String(imageBytes);
                    }
                }
                if (string.IsNullOrEmpty(post.ImageBase64))
                {
                    return Json(new { success = false, message = "ImageBase64 is null or empty." });
                }

                _db.Posts.Add(post);
                _db.SaveChanges();

                // Điều hướng người dùng đến trang hiển thị bài viết đã đăng
                return Json(new { success = true, imageUrl = post.ImageURL, imageBase64 = post.ImageBase64, category = post.Category, title = post.Title, content = post.Content, createdAt = post.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss"), username = userName });
            }
            else
            {
                return Json(new { success = false, message = "Model state is invalid." });
            }
        }
        [HttpPost]
        public ActionResult Update(int postId, string category, string title, string content, IFormFile imageURL)
        {
            var postToUpdate = _db.Posts.FirstOrDefault(p => p.PostID == postId);
            if (postToUpdate == null)
            {
                return Json(new { success = false, message = "Post not found." });
            }

            // Cập nhật thông tin mới cho bài viết
            postToUpdate.Category = category;
            postToUpdate.Title = title;
            postToUpdate.Content = content;

            // Kiểm tra và cập nhật ảnh mới nếu có
            if (imageURL != null && imageURL.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    imageURL.CopyTo(memoryStream);
                    byte[] imageBytes = memoryStream.ToArray();
                    postToUpdate.ImageBase64 = Convert.ToBase64String(imageBytes);
                }
            }

            _db.SaveChanges();

            // Trả về thông tin của bài viết đã được cập nhật
            return RedirectToAction("Index", "Account");
        }

    }
}
