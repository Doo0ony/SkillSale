using SkillSale.Areas.Identity.Data;
using SkillSale.Enums;
using System.ComponentModel.DataAnnotations;

namespace SkillSale.Models
{
    public class Vacancy
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; } = String.Empty;
        [Required]
        public int Salary { get; set; }
        [Required]
        public int Experience { get; set; }
        [Required]
        public WorkStatus WorkStatus { get; set; }
        [Required]
        public string Address { get; set; } = String.Empty;
        [Required]
        public string Description { get; set; } = String.Empty;

        public bool IsAvaliable { get; set; } = true;
        public SkillSaleUser? Author;
        public List<SkillSaleUser>? CandidatesList;
    }
}
