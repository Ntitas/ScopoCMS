using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ScopoCMS.Web.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ScopoCMS.Web.Models;
using ScopoCMS.Web.Services;
using ScopoCMS.Web.ViewModels;

namespace ScopoCMS.Web.Controllers
{
  

    public class HomeController : Controller
    {
        private PostService postService;
        private SectionService sectionService;
       
       
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger,PostService postService,SectionService sectionService)
        {
            this.sectionService = sectionService;
            this.postService = postService;
            _logger = logger;
        }
       
        public IActionResult Index()
        {
            List<PostViewModel> postVM = new List<PostViewModel>();
          

            var pm = (from r in sectionService.getPostinSection()
                     join p in postService.getAllPosts() on r.PostID equals p.postID
                     join s in sectionService.getAllSection() on r.SectionID equals s.sectionId
                     select new PostViewModel
                     {
                         postID = p.postID,
                         author = p.author,
                         title = p.title,
                         publishDate = p.publishDate,
                         sectionId = s.sectionId,
                         sectionname = s.name,
                         description = p.description,
                         imagePath = p.imagePath
                     }).ToList();

           
           
            return View();
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
