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
        public string Title { get; set; } = string.Empty;

        [Required]
        public int Salary { get; set; }

        [Required]
        public string CompanyName { get; set; } = string.Empty;

        [Required]
        public int Experience { get; set; }

        [Required]
        public WorkStatus WorkStatus { get; set; }

        [Required]
        public string Address { get; set; } = string.Empty;

        [Required]
        public Location Location { get; set; }

        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        [Phone]
        public string Phone { get; set; }
        public bool IsAvaliable { get; set; } = true;

        // Внешний ключ на автора вакансии
        public string? AuthorId { get; set; }
        public SkillSaleUser? Author { get; set; }

        public List<SkillSaleUser>? CandidatesList { get; set; }
    }
}
