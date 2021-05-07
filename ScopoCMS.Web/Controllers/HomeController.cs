using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ScopoCMS.Web.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.Extensions.Options;
using ScopoCMS.Web.ViewModels;
using Microsoft.Extensions.Configuration;
using System.IO;
using System;
using Microsoft.AspNetCore.Hosting;

namespace ScopoCMS.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly CMSDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _appEnvironment;


        public HomeController(ILogger<HomeController> logger, CMSDbContext context, IConfiguration configuration,IWebHostEnvironment appEnvironment)
        {
            _logger = logger;
            _context = context;
            _configuration = configuration;
            _appEnvironment = appEnvironment;


        }


        public async Task<IActionResult> Index()
        {
            var themeName = _configuration.GetSection("MySettings").GetSection("ThemeName").Value;

            var cMSDbContext = _context.Posts.Include(p => p.Category);
            ViewData["CatList"] = await _context.Categories.ToListAsync();

            return View("Views/Shared/Themes/" + themeName + "/Index.cshtml", await cMSDbContext.ToListAsync());
        }

        [Authorize(Roles = "Admin")]
        public IActionResult AdminPanel()
        {
            return View();
        }
        public async Task<IActionResult> ShowPostsByCategory(int id)
        {
            var themeName = _configuration.GetSection("MySettings").GetSection("ThemeName").Value;
            var cMSDbContext = _context.Posts.Where(p => p.CategoryID == id).Include(p => p.Category).ToListAsync();
            ViewData["CatList"] = await _context.Categories.ToListAsync();

            return View("Views/Shared/Themes/" + themeName + "/ShowPostsByCategory.cshtml", await cMSDbContext);
        }
        public async Task<IActionResult> PostDetails(int id)
        {
            var themeName = _configuration.GetSection("MySettings").GetSection("ThemeName").Value;
            var cMSDbContext = _context.Posts.Where(p => p.PostID == id).Include(p => p.Category).FirstOrDefaultAsync();

            ViewData["CatList"] = await _context.Categories.ToListAsync();
            return View("Views/Shared/Themes/" + themeName + "/PostDetails.cshtml", await cMSDbContext);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult MediaGallery()
        {
            return View();
        }

  
        [HttpPost]
        public IActionResult MediaGallery(MediaGalleryViewModel model)
        {
          

            foreach (var image in model.images)
            {


                var imgpath = Path.Combine(_appEnvironment.WebRootPath, "Images");
                var fileName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(image.FileName);
                using (var fileStream = new FileStream(Path.Combine(imgpath, fileName), FileMode.Create))
                {
                    image.CopyToAsync(fileStream);
                    string filePath = "uploads\\img\\" + fileName;
                }
            }
                return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
