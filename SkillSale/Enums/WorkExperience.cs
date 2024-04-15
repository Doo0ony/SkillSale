using System.ComponentModel.DataAnnotations;

namespace SkillSale.Enums
{
    public enum WorkExperience
    {
        [Display(Name = "Нет опыта")]
        NoExperience,

        [Display(Name = "До года")]
        UpToOneYear,

        [Display(Name = "До трех лет")]
        UpToThreeYears,

        [Display(Name = "Свыше трех лет")]
        OverThreeYears,
    }
}
