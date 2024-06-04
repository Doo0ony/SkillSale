using Microsoft.AspNetCore.Identity;
using SkillSale.Models;
using System.ComponentModel.DataAnnotations;

namespace SkillSale.Areas.Identity.Data;

public class SkillSaleUser : IdentityUser
{
    [Required]
    public string FirstName { get; set; } = String.Empty;
    [Required]
    public string LastName { get; set; } = String.Empty;
    [Required]
    public DateTime Age { get; set; }
    [Required]
    public DateTime DateCreated { get; set; }

    public string ProfileImage { get; set; }

    public List<Resume>? Resumes { get; set; }
    public List<Vacancy>? Vacancies { get; set; }

    public List<FavoriteResumes>? FavoriteResumes { get; set; }
    public List<FavoriteVacancies>? FavoriteVacancies { get; set; }
}

