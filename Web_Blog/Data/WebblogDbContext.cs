using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Web_Blog.Models;

namespace Web_Blog.Data
{
    public class WebblogDbContext : DbContext
    {
        public WebblogDbContext(DbContextOptions<WebblogDbContext> options) : base(options)
        {

        }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("Users");
           

            base.OnModelCreating(modelBuilder);

        }
    }
}
