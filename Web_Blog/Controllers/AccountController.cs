using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Web_Blog.Data;
using Web_Blog.Models;
using Microsoft.AspNetCore.Http;
using System.Security.Cryptography;
using System.Text;
namespace Web_Blog.Controllers
{
    public class AccountController : Controller
    {
        private readonly WebblogDbContext _db;
        


        public AccountController(WebblogDbContext db)
        {
            _db = db;
     
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
        public ActionResult Index()
        {


  
               if (HttpContext.Session.GetInt32("idUser") != null)
               {

                   string userName = HttpContext.Session.GetString("username");

                   ViewBag.UserName = userName;
                   return View();
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
