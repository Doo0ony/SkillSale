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
        [Length(0,100, ErrorMessage = "Не более 100 символов!")]
        public string DesiredPosition { get; set; } = String.Empty;
        [Required]
        [Range(0, 1_000_000, ErrorMessage = "Неправильный ввод!")]
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
        [Phone]
        public string? Phone { get; set; }
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        [Length(100, 1000, ErrorMessage = "Не менее 100 символов!")]
        public string AboutMe { get; set; } = String.Empty;
        

        // Внешний ключ на автора резюме
        public string? AuthorId { get; set; }
        public SkillSaleUser? Author { get; set; }

        public List<SkillSaleUser>? EmployerCandidates { get; set; }
    }
}
