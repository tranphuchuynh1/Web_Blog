using Microsoft.EntityFrameworkCore;
using Web_Blog.Data;
using Web_Blog.Models.Interfaces;

namespace Web_Blog.Models.Services
{
    public class createpostRepository : IcreatepostRepository
    {
        private readonly WebblogDbContext _dbContext;
        public createpostRepository(WebblogDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public IEnumerable<Post> getallpost()
        {
            return _dbContext.Posts.Include(p => p.User) // Include User information
                                   .Select(p => new Post
                                   {
                                       PostID = p.PostID,
                                       Category = p.Category,
                                       Title = p.Title,
                                       Content = p.Content,
                                       CreatedAt = p.CreatedAt,
                                       ImageBase64 = p.ImageBase64,
                                       ImageURL = p.ImageURL,
                                       idUser = p.idUser,
                                       UserAvatar = p.User.Avatar,
                                       PostedBy = p.User != null ? p.User.username : "Unknown"
                                   })
                                   .ToList();
        }


    }
}
