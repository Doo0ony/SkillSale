using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Shared;
using SkillSale.Areas.Identity.Data;
using System.Drawing.Imaging;
using System.Drawing;
using SkillSale.Data;


namespace SkillSale.Areas.Identity.Pages.Account.Manage
{
    public class ProfileImageModel : PageModel
    {
        private readonly UserManager<SkillSaleUser> _userManager;
        public string profileImageUrl;

        private readonly SkillSaleContext _context;

        public ProfileImageModel(SkillSaleContext context,  UserManager<SkillSaleUser> userManager){
            _userManager = userManager;  
            _context = context;
        }

        public async Task<IActionResult> OnGet()
        {
            var currentUser = await _userManager.GetUserAsync(User);

            profileImageUrl = currentUser.ProfileImage;
            return Page();
        }

        public async Task OnPost(IFormFile file) {
            if (file != null && file.Length > 0) {
                try
                {
                    var currentUser = await _userManager.GetUserAsync(User);

                    var deleteFilename = currentUser.ProfileImage;
                    string extension = Path.GetExtension(file.FileName);
                    string filename = Guid.NewGuid().ToString() + extension;

                    string savePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "profilePictures", filename);
                    string delPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", deleteFilename);

                    await Console.Out.WriteLineAsync(savePath);
                    await Console.Out.WriteLineAsync(delPath);
                    Directory.CreateDirectory(Path.GetDirectoryName(savePath));

                    if (System.IO.File.Exists(delPath))
                        System.IO.File.Delete(delPath);

                    using (var stream = new FileStream(savePath, FileMode.Create)) {
                        await file.CopyToAsync(stream);
                    }

                    profileImageUrl = "profilePictures/" + filename;
                    currentUser.ProfileImage = profileImageUrl;

                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    await Console.Out.WriteLineAsync( ex.Message);
                }
            }
        }

       
    }
}
