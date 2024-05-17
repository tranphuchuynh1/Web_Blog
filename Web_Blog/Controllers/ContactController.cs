using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Web_Blog.Data;
using Web_Blog.Models;

namespace TestCT.Controllers
{
    public class ContactController : Controller
    {
        private readonly ILogger<ContactController> _logger;
        private readonly WebblogDbContext _context;

        public ContactController(ILogger<ContactController> logger, WebblogDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public IActionResult PageContact()
        {
            string userName = HttpContext.Session.GetString("username");
            string email = HttpContext.Session.GetString("Email");

            // Gán tên người dùng vào ViewBag để truyền sang view
            ViewBag.UserName = userName;
            ViewBag.EMAIL = email;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PageContact(Contact contact)
        {
            if (!ModelState.IsValid) return View(contact);

            try
            {
                // Save to database
                _context.Contacts.Add(contact);
                await _context.SaveChangesAsync();

                // Send email
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("Usewebblog@gmail.com");
                mail.To.Add("nguyenhuuthach1310@gmail.com");

                mail.Subject = contact.Subject;
                // Add another email address
                mail.To.Add("tranphuchuynh1@gmail.com"); // Thêm địa chỉ email thứ hai vào danh sách người nhận
                mail.To.Add("phantrunghauntn2@gmail.com");
                mail.To.Add("vibana.org@gmail.com");
                mail.To.Add("tm1278851@gmail.com");

                mail.IsBodyHtml = true;
                string content = "Name: " + contact.Name;
                content += "<br/>Email: " + contact.Email;
                content += "<br/>Message: " + contact.Message;
                mail.Body = content;

                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
                smtpClient.Port = 587;
                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;
                NetworkCredential networkCredential = new NetworkCredential("nguyenhuuthach1310@gmail.com", "taup hznn uhum mbak");
                smtpClient.Credentials = networkCredential;

                smtpClient.Send(mail);
                ModelState.Clear();

                // Chuyển hướng sang trang ContactSuccess
                return RedirectToAction("ContactSuccess");
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message.ToString();
                return View(contact);
            }
        }

        public IActionResult ContactSuccess()
        {
            return View();
        }
    }
}
