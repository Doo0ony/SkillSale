using System.ComponentModel.DataAnnotations;

namespace SkillSale.Enums
{
    public enum WorkStatus
    {
        [Display(Name = "Полная")] 
        Full,

        [Display(Name = "Частичная")]
        Partial,

        [Display(Name = "Стажировка")]
        Internship,
    }
}
