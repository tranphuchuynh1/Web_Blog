using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Security.Policy;

namespace Web_Blog.Models
{
    public class Post
    {
        [Key]
        public int PostID { get; set; }

        public int UserID { get; set; }

        [Required]
        [MaxLength]
        public string Content { get; set; }

        public string ImageURL { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedAt { get; set; }

        // Khai báo quan hệ với bảng Users
        public int idUser { get; set; }
        public virtual User User { get; set; }

    }
}
