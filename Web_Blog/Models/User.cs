using System.ComponentModel.DataAnnotations;

namespace Web_Blog.Models
{
    public class User
    {
        [Key]
        public int userId { get; set; } 
        public string? username { get; set; }
        
        public string? password { get; set; }

        public string? Email { get; set; }
        public byte[]  Avartar { get; set; }

    }
}
