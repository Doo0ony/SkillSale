using System.ComponentModel.DataAnnotations;

namespace SkillSale.Models
{
    public class Feedback
    {
        [Key]
        public int FeedBackId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        public string Message { get; set; }
    }
}
