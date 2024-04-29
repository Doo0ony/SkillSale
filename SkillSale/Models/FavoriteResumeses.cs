using SkillSale.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SkillSale.Models
{
    public class FavoriteResumes
    {
        [Key]
        public Guid FavoriteResumeId { get; set; }

        [Required]
        public Guid AuthorId { get; set; }

        public SkillSaleUser? Author { get; set; }

        [Required]
        public Guid ResumeId { get; set; }

        public Resume? Resume { get; set; }
    }
}
