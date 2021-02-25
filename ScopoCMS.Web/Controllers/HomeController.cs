using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ScopoCMS.Web.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ScopoCMS.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly CMSDbContext _context;

        public HomeController(ILogger<HomeController> logger ,  CMSDbContext context)
        {
            _logger = logger;
             _context = context;
        }

        
        public async Task<IActionResult> Index()
        {
            var cMSDbContext = _context.Posts.Include(p => p.Category);
            ViewData["CatList"] = await _context.Categories.ToListAsync();

            return View(await cMSDbContext.ToListAsync());
        }

        [Authorize(Roles = "Admin")]
        public IActionResult AdminPanel()
        {
            return View();
        }
 
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
