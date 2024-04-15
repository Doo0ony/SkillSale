using Microsoft.AspNetCore.Identity;
using SkillSale.Models;

namespace SkillSale.Areas.Identity.Data;

public class SkillSaleUser : IdentityUser
{
    public string FirstName { get; set; } = String.Empty;
    public string LastName { get; set; } = String.Empty;
    public int Age { get; set; }

    public List<Resume>? Resumes { get; set; }
    public List<Vacancy>? Vacancies { get; set; }
}

