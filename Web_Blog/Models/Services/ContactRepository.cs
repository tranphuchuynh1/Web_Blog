using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web_Blog.Data;
using Web_Blog.Models.Interfaces;
using Web_Blog.Models;
namespace Web_Blog.Models.Services

{
    public class ContactRepository : IContactRepository
    {
        private readonly WebblogDbContext _context;

        public ContactRepository(WebblogDbContext context)
        {
            _context = context;
        }

        public async Task AddContactAsync(Contact contact)
        {
            _context.Contacts.Add(contact);
            await _context.SaveChangesAsync();
        }
    }
}
