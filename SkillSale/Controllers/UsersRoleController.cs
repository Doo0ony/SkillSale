
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
                                   ProfilePicture = user.ProfileImage,
                                   UserName = user.FirstName,
                                   RoleId = userRole.RoleId,
                                   RoleName = role.Name
                               }).ToListAsync()) ;
        }

        public async Task<IActionResult> SetRole(string id, string role)
        {
            // Найти роль по названию
            var roleEntity = await _context.Roles.FirstOrDefaultAsync(r => r.Name == role);
            if (roleEntity == null)
            {
                return RedirectToAction(nameof(Index), new { error = "Роль не найдена." });
            }

            // Получить пользователя по ID
            var user = await _context.SkillSaleUsers.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            // Получить текущие роли пользователя
            var userRoles = await _context.UserRoles.Where(ur => ur.UserId == id).ToListAsync();

            // Удаление всех текущих ролей пользователя
            _context.UserRoles.RemoveRange(userRoles);

            try
            {
                // Добавление новой роли для пользователя
                var newUserRole = new IdentityUserRole<string>
                {
                    UserId = id,
                    RoleId = roleEntity.Id
                };
                _context.UserRoles.Add(newUserRole);

                // Сохранение изменений
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(Index), new { error = "Ошибка при сохранении изменений." });
            }

            return Ok();
        }


    }
}
