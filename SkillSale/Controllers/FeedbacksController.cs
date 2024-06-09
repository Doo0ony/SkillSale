using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SkillSale.Data;
using SkillSale.Models;

namespace SkillSale.Controllers
{
    public class FeedbacksController : Controller
    {
        private readonly SkillSaleContext _context;

        public FeedbacksController(SkillSaleContext context)
        {
            _context = context;
        }

        // GET: Feedbacks
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([Bind("FeedBackId,Name,Email,Subject,Message")] Feedback feedback)
        {
            if (ModelState.IsValid)
            {
                _context.Add(feedback);
                await _context.SaveChangesAsync();
            }
            return View(feedback);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Feedbacks() 
        {
            var feedbacks = await _context.Feedbacks.ToListAsync();

            return View(feedbacks);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Delete(int id) {
            var feedback = await _context.Feedbacks.FirstOrDefaultAsync(x => x.FeedBackId == id);

            return View(feedback);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            var feedback = await _context.Feedbacks.FirstOrDefaultAsync(x => x.FeedBackId == id);

            if (feedback == null)
                return NotFound();

            _context.Feedbacks.Remove(feedback);

            await _context.SaveChangesAsync();

            return RedirectToAction("Feedbacks");
        }
    }
}
