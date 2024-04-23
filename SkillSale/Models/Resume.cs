using SkillSale.Areas.Identity.Data;
using SkillSale.Enums;
using System.ComponentModel.DataAnnotations;

namespace SkillSale.Models
{
    public class Resume
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string DesiredPosition { get; set; } = String.Empty;
        [Required]
        public int Salary { get; set; }
        [Required]
        public Location Location { get; set; }
        [Required]
        public WorkStatus WorkStatus { get; set; }
        [Required]
        public EducationLevel EducationLevel { get; set; }
        [Required]
        public WorkExperience WorkExperience { get; set; }
        [Required]
        public string? Phone { get; set; }
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        public string AboutMe { get; set; } = String.Empty;

        // Внешний ключ на автора резюме
        public string? AuthorId { get; set; }
        public SkillSaleUser? Author { get; set; }

        public List<SkillSaleUser>? EmployerCandidates { get; set; }
    }
}
