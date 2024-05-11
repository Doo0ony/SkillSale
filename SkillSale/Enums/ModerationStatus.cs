using System.ComponentModel.DataAnnotations;

namespace SkillSale.Enums
{
    public enum ModerationStatus
    {
        [Display(Name = "На модерации")]
        UnderReview,

        [Display(Name = "Подтверждено")]
        Approved,

        [Display(Name = "Отклонено")]
        Rejected,
    }
}
