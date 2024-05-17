using Microsoft.AspNetCore.Mvc;
using Web_Blog.Data;
using Web_Blog.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System.Diagnostics;
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

                int? userId = HttpContext.Session.GetInt32("idUser");

                Post post = new Post
                {
                    Category = Category,
                    Title = Title,
                    Content = Content,
                    CreatedAt = DateTime.Now,
                    idUser = userId.Value
                };
                if (ImageURL != null && ImageURL.Length > 0)
                {
                    string fileName = Category + Path.GetExtension(ImageURL.FileName);
                    string filePath = @"wwwroot\ProductImages\" + fileName;


                    var directoryLocation = Path.Combine(Directory.GetCurrentDirectory(), filePath);
                    FileInfo file = new FileInfo(directoryLocation);

                    var filePathDirectory = Path.Combine(Directory.GetCurrentDirectory(), filePath);
                    using (var fileStream = new FileStream(filePathDirectory, FileMode.Create))
                    {
                        ImageURL.CopyTo(fileStream);
                    }
                    var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}{HttpContext.Request.PathBase.Value}";
                    post.ImageURL = baseUrl + "/ProductImages/" + fileName;
                }
                _db.Posts.Add(post);
                _db.SaveChanges();

                // Điều hướng người dùng đến trang hiển thị bài viết đã đăng
                return RedirectToAction("Index", "Account");
            }
            else
            {

                return RedirectToAction("Login", "Account");
            }

        }


    }


}