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
            return _dbContext.Posts;
        }
        /*
        public void Clear(int postId)
        {
            var post = _dbContext.Posts.Find(postId);
            if (post != null)
            {
                _dbContext.Posts.Remove(post);
                _dbContext.SaveChanges();
            }
        }
        public Post? DeleteAuthorById(int id)
        {
            var authorDomain = _dbContext.Posts.FirstOrDefault(n => n.PostID == id);
            if (authorDomain != null)
            {
                _dbContext.Posts.Remove(authorDomain);
                _dbContext.SaveChanges();
            }
            return null;*/
    }
}
