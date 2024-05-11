using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;
using SkillSale.Areas.Identity.Data;
using SkillSale.Enums;

namespace SkillSale.Models
{
    public class Vacancy
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [Length(0, 100, ErrorMessage ="Не более 100 символов!")]
        public string Title { get; set; } = string.Empty;

        [Required]
        [Range(0, 1_000_000, ErrorMessage = "Неправильный ввод!")]
        public int Salary { get; set; }

        [Required]
        [Length(0, 100, ErrorMessage = "Не более 100 символов!")]
        public string CompanyName { get; set; } = string.Empty;

        [Required]
        public WorkExperience Experience { get; set; }

        [Required]
        public WorkStatus WorkStatus { get; set; }

        [Required]
        [Length(0, 50, ErrorMessage = "Не более 50 символов!")]
        public string Address { get; set; } = string.Empty;

        [Required]
        public Location Location { get; set; }

        [Required]
        [Length(100, 1000, ErrorMessage = "Не менее 100 символов!")]
        public string Description { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        [Phone]
        public string? Phone { get; set; }
        public bool IsAvaliable { get; set; } = true;

        // Модерация (видимость вакансии на сайте)
        public ModerationStatus ModerationStatus { get; set; } = ModerationStatus.UnderReview;

        // Внешний ключ на автора вакансии
        public string? AuthorId { get; set; }
        public SkillSaleUser? Author { get; set; }

        public List<SkillSaleUser>? CandidatesList { get; set; }
    }
}
