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
