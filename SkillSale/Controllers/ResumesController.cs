using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkillSale.Areas.Identity.Data;
using SkillSale.Data;
using SkillSale.Models;
using System.Security.Claims;

namespace SkillSale.Controllers
{
    public class ResumesController : Controller
    {
        private readonly SkillSaleContext _context;
        private readonly UserManager<SkillSaleUser> _userManager;
   
        public ResumesController(SkillSaleContext context, UserManager<SkillSaleUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        // GET: Resumes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Resumes
                .Where(d => d.ModerationStatus == Enums.ModerationStatus.Approved)
                .Include(x => x.Author)
                .ToListAsync());
        }

        // GET: Resumes/Details/5

        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resume = await _context.Resumes
                .Include(x => x.Author)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (resume == null)
            {
                return NotFound();
            }

            return View(resume);
        }

        // GET: Resumes/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Resumes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,DesiredPosition,Salary,EducationLevel,WorkExperience,AboutMe,WorkStatus, Phone, Email, Location")] Resume resume)
        {
            if (ModelState.IsValid)
            {
                resume.Id = Guid.NewGuid();

                resume.Author = await (_userManager.GetUserAsync(User));
                resume.AuthorId = resume.Author.Id;
                
                _context.Add(resume);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(resume);
        }

        // GET: Resumes/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resume = await _context.Resumes.FindAsync(id);

            if (resume == null)
            {
                return NotFound();
            }

            var currentUser = _userManager.GetUserAsync(User).Result;

            if (resume.Author != currentUser)
                return NotFound();

            return View(resume);
        }

        // POST: Resumes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,DesiredPosition,Salary,EducationLevel,WorkExperience,AboutMe,WorkStatus, Phone, Email, Location")] Resume resume)
        {
            if (id != resume.Id)
            {
                return NotFound();
            }

            var currentUser = _userManager.GetUserAsync(User).Result;

            if (resume.Author != currentUser)
                return NotFound();

            resume.Author = await (_userManager.GetUserAsync(User));
            resume.AuthorId = resume.Author.Id;
            

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(resume);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ResumeExists(resume.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(resume);
        }

        // GET: Resumes/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var currentUser = _userManager.GetUserAsync(User).Result;

            var resume = await _context.Resumes
                .FirstOrDefaultAsync(m => m.Id == id);

            if (resume == null)
            {
                return NotFound();
            }

            var role = _userManager.GetRolesAsync(currentUser).Result.FirstOrDefault();

            if ((resume.Author != currentUser) && (role != "Admin"))
                return NotFound();

            return View(resume);
        }

        // POST: Resumes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var resume = await _context.Resumes.FindAsync(id);

            var currentUser = _userManager.GetUserAsync(User).Result;
            var role = _userManager.GetRolesAsync(currentUser).Result.FirstOrDefault();

            if ((resume.Author != currentUser) && (role != "Admin"))
                return NotFound();

            if (resume != null)
            {
                _context.Resumes.Remove(resume);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), "Profile");
        }

        private bool ResumeExists(Guid id)
        {
            return _context.Resumes.Any(e => e.Id == id);
        }

        [HttpPost]
        [Authorize]
        public async Task AddToFavoriteList(string value)
        {
            Guid resumeId = Guid.Parse(value);

            var resume = await _context.Resumes.FirstOrDefaultAsync(x => x.Id == resumeId);

            if (resume == null)
                return;

            var currentUser = await _userManager.GetUserAsync(User);

            if (currentUser == null)
                return;

            await _context.Entry(currentUser)
                .Collection(u => u.FavoriteResumes)
                .LoadAsync();

            try
            {
                var likedResume = currentUser.FavoriteResumes.FirstOrDefault(x => x.ResumeId == resume.Id);

                if (likedResume != null)
                {
                    currentUser.FavoriteResumes.Remove(likedResume);
                    await _context.SaveChangesAsync();

                }
                else
                {
                    currentUser.FavoriteResumes.Add(new FavoriteResumes
                    {
                        AuthorId = Guid.Parse(currentUser.Id),
                        ResumeId = resume.Id
                    });
                    await _context.SaveChangesAsync();
                }

            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
            }
        }

    }
}
