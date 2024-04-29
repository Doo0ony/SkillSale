using SkillSale.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SkillSale.Models
{
    public class FavoriteVacancies
    {
        [Key]
        public Guid FavoriteVacancyId { get; set; }

        [Required]
        public Guid AuthorId { get; set; }

        public SkillSaleUser? Author { get; set; }

        [Required]
        public Guid VacancyId { get; set; }

        public Vacancy? Vacancy { get; set; }
    }
}
