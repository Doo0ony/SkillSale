using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SkillSale.Areas.Identity.Data;
using SkillSale.Models;
using System.Reflection.Emit;

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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Vacancy>()
            .HasOne(v => v.Author)
            .WithMany(u => u.Vacancies)
            .HasForeignKey(v => v.AuthorId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Resume>()
            .HasOne(r => r.Author)
            .WithMany(u => u.Resumes)
            .HasForeignKey(r => r.AuthorId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
    }

}
