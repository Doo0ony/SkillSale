
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SkillSale.Areas.Identity.Data;
using SkillSale.Data;
using SkillSale.Models;

namespace MarketPlace.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersRoleController : Controller
    {
        private readonly SkillSaleContext _context;
        private readonly UserManager<SkillSaleUser> _userManager;

        public UsersRoleController(SkillSaleContext context, UserManager<SkillSaleUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        // GET: UsersRoleController
        public async Task<IActionResult> Index()
        {
            return View(await (from user in _context.SkillSaleUsers
                               join userRole in _context.UserRoles on user.Id equals userRole.UserId
                               join role in _context.Roles on userRole.RoleId equals role.Id
                               select new UserWithRole
                               {
                                   UserId = user.Id,
                                   UserLastName = user.LastName,
                                   Email = user.Email,
                                   UserName = user.FirstName,
                                   RoleId = userRole.RoleId,
                                   RoleName = role.Name
                               }).ToListAsync()) ;
        }

        // GET: /User/EditRole/{userId}
        public IActionResult Edit(string userId)
        {
            // Получаем пользователя и его текущую роль из базы данных
            var userWithRole = (from user in _context.SkillSaleUsers
                                join userRole in _context.UserRoles on user.Id equals userRole.UserId
                                join role in _context.Roles on userRole.RoleId equals role.Id
                                where user.Id == userId
                                select new UserWithRole
                                {
                                    UserId = user.Id,
                                    UserName = user.FirstName,
                                    RoleId = userRole.RoleId,
                                    RoleName = role.Name
                                }).FirstOrDefault();

            if (userWithRole == null)
                return NotFound();

            // Загружаем список ролей для использования в выпадающем списке в представлении
            var roles = _context.Roles.ToList();
            ViewBag.Roles = new SelectList(roles, "Id", "Name", userWithRole.RoleId);

            return View(userWithRole);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string userId, string roleId)
        {
            // Получаем пользователя по ID
            var user = await _context.SkillSaleUsers.FindAsync(userId);

            if (user == null)
                return NotFound();

            // Получаем текущую роль пользователя
            var userRoles = await _context.UserRoles.Where(ur => ur.UserId == userId).ToListAsync();

            // Удаление всех текущих ролей пользователя
            _context.UserRoles.RemoveRange(userRoles);

            try
            {
                // Добавление новой роли для пользователя
                var newUserRole = new IdentityUserRole<string>
                {
                    UserId = userId,
                    RoleId = roleId
                };
                _context.UserRoles.Add(newUserRole);

                // Сохранение изменений
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(Index),
                    new { error = "Ошибка при сохранении изменений." });
            }

            return RedirectToAction(nameof(Index));
        }

    }
}
