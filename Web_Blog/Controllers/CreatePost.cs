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
                // Kiểm tra xem người dùng đã đăng nhập chưa
                if (User.Identity.IsAuthenticated)
                {
                    // Lấy ID của người dùng hiện tại
                    int userId = int.Parse(User.Identity.Name); // Giả sử ID người dùng được lưu trong Name của Identity


                    var post = new Post
                    {
                        UserID = userId,
                        Category = Category,
                        Title = Title,
                        Content = Content,
                        CreatedAt = DateTime.Now
                    };
                    if (ImageURL != null && ImageURL.Length > 0)
                    {
                        // Lưu trữ ảnh vào thư mục và lấy đường dẫn của ảnh
                        string uploadsFolder = Path.Combine(_environment.WebRootPath, "upload");
                        string uniqueFileName = Guid.NewGuid().ToString() + "_" + ImageURL.FileName;
                        string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            ImageURL.CopyTo(fileStream);
                        }
                        post.ImageURL = "~/upload/" + uniqueFileName;
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

            // Nếu dữ liệu không hợp lệ, hiển thị form đăng bài viết lại với thông báo lỗi
            return View("_CreatePostPartial");
        }

    }
}
    