using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ScopoCMS.Web.Services;
using ScopoCMS.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ScopoCMS.Web.Controllers
{


    public class SectionController : Controller
    {
        private PostService postService;
        private SectionService sectionService;
        public SectionController(PostService postService,SectionService sectionService)
        {
            this.postService = postService;
            this.sectionService = sectionService;
        }
        public IActionResult Index()
        {

            return View();
        }
        public IActionResult Create()
        {

            var res = postService.getAllPosts();
            ViewBag.list = new SelectList(res, "postID", "title");


            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult Create(Section section)
        {
            if (ModelState.IsValid)
            {
                sectionService.Create(section);
                     return RedirectToAction(nameof(Index));

            }
            return View();
        }
    }
}
