using System.ComponentModel.DataAnnotations;

namespace SkillSale.Enums
{
    public enum WorkStatus
    {
        [Display(Name = "Полная занятость")] 
        Full,

        [Display(Name = "Частичная занятость")]
        Partial,

        [Display(Name = "Стажировка")]
        Internship,
        
        [Display(Name = "Фриланс")]
        Freelance,

        [Display(Name = "Подработка")]
        Temporary,
	}
}
