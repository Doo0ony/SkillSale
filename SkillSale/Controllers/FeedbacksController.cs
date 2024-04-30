using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    }
}
