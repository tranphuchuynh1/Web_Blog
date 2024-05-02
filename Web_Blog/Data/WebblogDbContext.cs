using Microsoft.EntityFrameworkCore;

namespace Web_Blog.Data
{
    public class WebblogDbContext : DbContext
    {
        public WebblogDbContext(DbContextOptions<WebblogDbContext> options) : base(options)
        {

        }
    }
}
