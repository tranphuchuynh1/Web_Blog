using System.ComponentModel.DataAnnotations;

namespace Web_Blog.Models
{
    public class Contact
    {
        [Key] // Xác định trường Id là khóa chính
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Message is required")]
        public string? Message { get; set; }

    }
}
