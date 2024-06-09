using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Shared;
using SkillSale.Areas.Identity.Data;
using System.Drawing.Imaging;
using System.Drawing;
using SkillSale.Data;
using Microsoft.EntityFrameworkCore;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System.IO;

namespace SkillSale.Areas.Identity.Pages.Account.Manage
{
    public class ProfileImageModel : PageModel
    {
        private readonly UserManager<SkillSaleUser> _userManager;
        public string profileImageUrl;

        [TempData]
        public string StatusMessage { get; set; }


        private readonly SkillSaleContext _context;

        public ProfileImageModel(UserManager<SkillSaleUser> userManager, SkillSaleContext context  ){
            _userManager = userManager;  
            _context = context;
        }

        public async Task<IActionResult> OnGet()
        {
            var currentUser = await _userManager.GetUserAsync(User);

            profileImageUrl = currentUser.ProfileImage;
            return Page();
        }

        public async Task OnPost(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                try
                {
                    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                    var ext = Path.GetExtension(file.FileName).ToLowerInvariant();

                    if (!allowedExtensions.Contains(ext))
                    {
                        profileImageUrl = "profilePictures/noimage.jpg";
                        StatusMessage = ("Файл не является изображением.");
                        RedirectToPage();
                        throw new Exception("Загруженный файл в картинку профиля, не является картинкой");
                        
                    }

                    var currentUser = await _userManager.GetUserAsync(User);

                    var deleteFilename = currentUser.ProfileImage;
                    string extension = Path.GetExtension(file.FileName);
                    string filename = Guid.NewGuid().ToString() + extension;

                    string savePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "profilePictures", filename);
                    string delPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", deleteFilename);

                    await Console.Out.WriteLineAsync(savePath);
                    await Console.Out.WriteLineAsync(delPath);
                    Directory.CreateDirectory(Path.GetDirectoryName(savePath));

                    string[] staticImageFiles = ["female.webp", "male.png", "noimage.jpg"];
                    
                    if (System.IO.File.Exists(delPath)) 
                        if (staticImageFiles.Contains(deleteFilename))
                            System.IO.File.Delete(delPath);

                    using (var stream = new FileStream(savePath, FileMode.Create))
                    {
                        // Изменение размера изображения до заданных размеров
                        using (var image = SixLabors.ImageSharp.Image.Load(file.OpenReadStream()))
                        {
                            var options = new ResizeOptions
                            {
                                Mode = ResizeMode.Max, // Изменение размера с сохранением пропорций
                                Size = new SixLabors.ImageSharp.Size(300, 300) // Заданные размеры (ширина, высота)
                            };
                            image.Mutate(x => x.Resize(options));
                            image.SaveAsJpeg(stream); // Сохранение изображения в формате JPEG
                        }
                    }

                    profileImageUrl = "profilePictures/" + filename;
                    currentUser.ProfileImage = profileImageUrl;

                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    await Console.Out.WriteLineAsync(ex.Message);
                }
            }
        }
    }
}
