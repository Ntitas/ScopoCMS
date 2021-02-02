using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ScopoCMS.Web.Services;
using ScopoCMS.Web.Models;
using ScopoCMS.Web.ViewModels;
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
            //var res = sectionService.getPostinSection();
            
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
        public  IActionResult Create(SectionCreateViewModel scvm)
        {
            if (ModelState.IsValid)
            {
             
                sectionService.Create(scvm);
                sectionService.CreateSectionTPost(scvm);
                //     return RedirectToAction(nameof(Index));

            }
            return View();
        }
        public IActionResult Edit()
        {

           
            ViewBag.list = new SelectList(postService.getAllPosts(), "postID", "title");
            ViewBag.sec = new SelectList(sectionService.getAllSection(), "sectionId", "name");
           


            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(SectionCreateViewModel scvm)
        {
            if(ModelState.IsValid)
            {
                var res = scvm.sectionId;
                sectionService.UpdateSectionContent(scvm);
               
            }
            return View();
        }

    }
}
