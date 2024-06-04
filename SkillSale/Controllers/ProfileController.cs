using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkillSale.Areas.Identity.Data;
using SkillSale.Data;
using SkillSale.Enums;
using System.Diagnostics;

namespace SkillSale.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly SkillSaleContext _context;
        private readonly UserManager<SkillSaleUser> _userManager;

        public ProfileController(SkillSaleContext context, UserManager<SkillSaleUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == User.Identity.Name);

            //footer off
            ViewBag.Footer = false;

            var result = await _context.SkillSaleUsers
                .Where(x => x.Id == currentUser.Id)
                .Include(x => x.Vacancies)
                    .ThenInclude(v => v.CandidatesList)
                .Include(x => x.Resumes)
                .ToListAsync();

            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> Portrait(string id) {

            if (id == null)
                return NotFound();

            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == id);

            var result = await _context.SkillSaleUsers
                .Where(x => x.Id == user.Id)
                .Include(x => x.Resumes.Where(r => r.ModerationStatus == ModerationStatus.Approved))
                .ToListAsync();

            return View(result);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> FavoriteVacancies() {

            //footer off
            ViewBag.Footer = false;

            var currentUser = await _userManager.GetUserAsync(User);

            if (currentUser == null)
                return NotFound(); 

            await _context.Entry(currentUser)
                .Collection(u => u.FavoriteVacancies)
                .Query()
                .Include(fv => fv.Vacancy) 
                .LoadAsync();

            var favoriteVacancies = currentUser.FavoriteVacancies
                .Select(fv => fv.Vacancy) 
                .ToList();          

            return View(favoriteVacancies);
        }
        
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> FavoriteResumes() {

            //footer off
            ViewBag.Footer = false;

            var currentUser = await _userManager
                .GetUserAsync(User);

            if (currentUser == null)
            {
                return NotFound(); 
            }

            await _context.Entry(currentUser)
                .Collection(u => u.FavoriteResumes)
                .Query()
                .Include(fr => fr.Resume) 
                .LoadAsync();

            var favoriteResumes = currentUser.FavoriteResumes
                .Select(fr => fr.Resume) 
                .ToList();

            return View(favoriteResumes);

        }
    }
}
