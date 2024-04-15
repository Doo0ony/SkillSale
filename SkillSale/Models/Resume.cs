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
        public int Salary { get; set;}
        [Required]
        public EducationLevel EducationLevel { get; set; }
        [Required]
        public WorkExperience WorkExperience { get; set; }
        [Required]
        public string AboutMe { get; set; } = String.Empty;

        public Guid AuthorId { get; set; }
        public SkillSaleUser? Author;
        public List<SkillSaleUser>? EmployerCandidates;
    }
}
