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
                Section section = new Section();
                section.sectionId = scvm.sectionId;
                section.name = scvm.name;
                List<Post> posts = new List<Post>();
               
                foreach(var i  in scvm.postId)
                {
                    var post = postService.getPostById(i);
                    posts.Add(post);

              

                }
                section.Posts = posts;
             
                sectionService.Create(section);
                sectionService.CreateSectionTPost(section);
                //     return RedirectToAction(nameof(Index));

            }
            return View();
        }
    }
}
