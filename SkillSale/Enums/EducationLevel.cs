using System.ComponentModel.DataAnnotations;

namespace SkillSale.Enums
{
    public enum EducationLevel
    {
        [Display(Name = "Среднее")]
        Secondary,

        [Display(Name = "Высшее")]
        Higher,

        [Display(Name = "Бакалавриат")]
        Bachelor,

        [Display(Name = "Магистратура")]
        Master,

        [Display(Name = "Докторантура")]
        PhD,
    }
}
