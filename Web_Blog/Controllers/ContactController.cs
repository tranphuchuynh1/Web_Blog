using Microsoft.AspNetCore.Mvc;
using Web_Blog.Models.Interfaces;
using Web_Blog.Models;

namespace Web_Blog.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactRepository _contactRepository;

        public ContactController(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

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
        public async Task<IActionResult> PageContact(Contact model)
        {
            
            if (ModelState.IsValid)
            {
                await _contactRepository.AddContactAsync(model);
                return RedirectToAction("ContactSuccess");
                
                
            }
            return View(model);
            
        }

        public IActionResult ContactSuccess()
        {
            return View();
        }
    }
}
