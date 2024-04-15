using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SkillSale.Areas.Identity.Data;
using SkillSale.Models;

namespace SkillSale.Data;

public class SkillSaleContext : IdentityDbContext<SkillSaleUser>
{
    public SkillSaleContext(DbContextOptions<SkillSaleContext> options)
        : base(options)
    {
    }

    public DbSet<SkillSaleUser> SkillSaleUsers { get; set; }
    public DbSet<Vacancy> Vacancies { get; set; }
    public DbSet<Resume> Resumes { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
}
