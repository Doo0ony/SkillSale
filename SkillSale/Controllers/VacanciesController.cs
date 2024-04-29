using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using SkillSale.Areas.Identity.Data;
using SkillSale.Data;
using SkillSale.Models;
using System.Security.Claims;

namespace SkillSale.Controllers
{
    public class VacanciesController : Controller
    {
        private readonly SkillSaleContext _context;
        private readonly UserManager<SkillSaleUser> _userManager;

        public VacanciesController(SkillSaleContext context, UserManager<SkillSaleUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Vacancies
        public async Task<IActionResult> Index()
        {
            return View(await _context.Vacancies.Include(x => x.Author).ToListAsync());
        }

        // GET: Vacancies/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var currentUser = await _userManager.GetUserAsync(User);

            var vacancy = await _context.Vacancies
                .Include(x => x.CandidatesList)
                .FirstOrDefaultAsync(m => m.Id == id);

            if(currentUser.Id == vacancy.AuthorId)
                ViewBag.CandidatesList = vacancy.CandidatesList;

            if (vacancy == null)
            {
                return NotFound();
            }

            return View(vacancy);
        }

        // GET: Vacancies/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Vacancies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,Title,CompanyName,Salary,WorkExperience,WorkStatus,Address,Description,IsAvaliable,Location, Phone, Email")] Vacancy vacancy)
        {
            if (ModelState.IsValid)
            {
                vacancy.Id = Guid.NewGuid();

                vacancy.Author = await (_userManager.GetUserAsync(User));
                vacancy.AuthorId = vacancy.Author.Id;

                _context.Add(vacancy);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vacancy);
        }

        // GET: Vacancies/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vacancy = await _context.Vacancies.FindAsync(id);
            if (vacancy == null)
            {
                return NotFound();
            }
            return View(vacancy);
        }

        // POST: Vacancies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Title,CompanyName,Salary,WorkExperience,WorkStatus,Address,Description,IsAvaliable,Location, Phone, Email")] Vacancy vacancy)
        {
            if (id != vacancy.Id)
            {
                return NotFound();
            }

            vacancy.Author = await (_userManager.GetUserAsync(User));
            vacancy.AuthorId = vacancy.Author.Id;


            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vacancy);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VacancyExists(vacancy.Id))
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
            return View(vacancy);
        }

        // GET: Vacancies/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vacancy = await _context.Vacancies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vacancy == null)
            {
                return NotFound();
            }

            return View(vacancy);
        }

        // POST: Vacancies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            // Находим вакансию по идентификатору
            var vacancy = await _context.Vacancies.Include(x => x.CandidatesList).FirstOrDefaultAsync(x => x.Id == id);

            if (vacancy == null)
            {
                return NotFound();
            }

            vacancy.CandidatesList.Clear();

            // Удаляем саму вакансию
            _context.Vacancies.Remove(vacancy);

            // Сохраняем изменения в базе данных
            await _context.SaveChangesAsync();

            // Перенаправляем пользователя на нужную страницу
            return RedirectToAction(nameof(Index), "Profile");
        }

        private bool VacancyExists(Guid id)
        {
            return _context.Vacancies.Any(e => e.Id == id);
        }

        [HttpPost]
        public async Task AddToFavoriteList(string value)
        {
            Guid vacancyId = Guid.Parse(value);

            var vacancy = await _context.Vacancies.FirstOrDefaultAsync(x => x.Id == vacancyId);

            if (vacancy == null)
                return;

            var currentUser = await _userManager.GetUserAsync(User);

            if (currentUser == null)
                return;

            await _context.Entry(currentUser)
                .Collection(u => u.FavoriteVacancies)
                .LoadAsync();

            try
            {
                var likedVacancy = currentUser.FavoriteVacancies.FirstOrDefault(x => x.VacancyId == vacancy.Id);

                if (likedVacancy != null)
                {
                    currentUser.FavoriteVacancies.Remove(likedVacancy);                   
                    await _context.SaveChangesAsync();

                }
                else
                {
                    currentUser.FavoriteVacancies.Add(new FavoriteVacancies
                    {
                        AuthorId = Guid.Parse(currentUser.Id),
                        VacancyId = vacancy.Id
                    });
                    await _context.SaveChangesAsync();
                }

            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
            }
        }

        [HttpPost]
        public async Task AddOrRemoveCandidateFromVacancy(string value)
        {
            Guid vacancyId = Guid.Parse(value);
            var vacancy = await _context.Vacancies.Include(v => v.CandidatesList)
                .FirstOrDefaultAsync(v => v.Id == vacancyId);

            if (vacancy == null)
                return;

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var user = await _context.Users.FindAsync(userId);

            if (user == null)
                return;

            if (vacancy.CandidatesList.Contains(user))
                vacancy.CandidatesList.Remove(user);
            else
                vacancy.CandidatesList.Add(user);

            await _context.SaveChangesAsync();
        }
    }
}
