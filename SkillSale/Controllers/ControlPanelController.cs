using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkillSale.Areas.Identity.Data;
using SkillSale.Data;
using SkillSale.Enums;

namespace SkillSale.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ControlPanelController : Controller
    {
        private readonly SkillSaleContext _context;
        private readonly UserManager<SkillSaleUser> _userManager;

        public ControlPanelController(SkillSaleContext context, UserManager<SkillSaleUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        // GET: ControlPanelController
        public async Task<ActionResult> Index()
        {
            //footer off
            ViewBag.Footer = false;

            var result1 = await _context.Vacancies
                .Include(v => v.CandidatesList)
                .ToListAsync();
            var result2 = await _context.Resumes.ToListAsync();

            ViewBag.Vacancies = result1;
            ViewBag.Resumes = result2;
            return View();
        }

        [Authorize]
        public async Task UpdateModerationStatusResumes(string id, string selectedStatus)
        {
            if (id == null)
                return;

            Guid idResume = Guid.Parse(id);

            if (selectedStatus == null)
                return;

            var resume = await _context.Resumes.FirstOrDefaultAsync(r => r.Id == idResume);

            ModerationStatus status = (ModerationStatus)Enum.Parse(typeof(ModerationStatus), selectedStatus);

            if(resume.ModerationStatus == status)
                return;

            resume.ModerationStatus = status;
            await _context.SaveChangesAsync();

            return;
        }

        [Authorize]
        public async Task UpdateModerationStatusVacancyes(string id, string selectedStatus)
        {
            if (id == null)
                return;

            Guid idVacancy = Guid.Parse(id);

            if (selectedStatus == null)
                return;

            var vacancy = await _context.Vacancies.FirstOrDefaultAsync(r => r.Id == idVacancy);

            ModerationStatus status = (ModerationStatus)Enum.Parse(typeof(ModerationStatus), selectedStatus);

            if(vacancy.ModerationStatus == status)
                return;

            vacancy.ModerationStatus = status;
            await _context.SaveChangesAsync();

            return;
        }
    }
}
