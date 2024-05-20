using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Security.Policy;

namespace Web_Blog.Models
{
    public class Post
    {
        [Key, Column(Order = 1)]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int PostID { get; set; }

        public string? Title { get; set; }
        public string? Category { get; set; }

        [Required]
        [MaxLength]
        public string? Content { get; set; }

        public string? ImageURL { get; set; }
        public string ImageBase64 { get; set; }
        public string PostedBy { get; set; }
        public string UserAvatar { get; set; }
        public DateTime CreatedAt { get; set; }

        // Khai báo quan hệ với bảng Users
        public int idUser { get; set; }
        public virtual User? User { get; set; }

    }
}
