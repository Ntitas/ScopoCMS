using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using ScopoCMS.Web.Data;
using ScopoCMS.Web.Models;
using ScopoCMS.Web.Services;

namespace ScopoCMS.Web.Controllers
{

    public class postsController : Controller
    {
        private PostService postService;
       private CategoryService categoryService;

        public postsController(PostService postService,CategoryService categoryService)
        {

            this.postService = postService;
           this.categoryService = categoryService;
        }

        // GET: posts
        public IActionResult Index()
        {
      
            return View( postService.getAllPosts());
        }

        // GET: posts/Details/5
        public IActionResult Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = postService.getPostById(id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // GET: posts/Create
        public IActionResult Create()
        {
           var list=categoryService.getAllCategories();
            ViewBag.list = new SelectList(list, "categoryID", "name");


            return View();
        }

        // POST: posts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Post post,IFormFile imageFile)
        {
            if (ModelState.IsValid)
            {
                var fileName = Path.GetFileName(imageFile.FileName);

                //Assigning Unique Filename (Guid)
                var myUniqueFileName = Convert.ToString(Guid.NewGuid());

                //Getting file Extension
                var fileExtension = Path.GetExtension(fileName);

                // concatenating  FileName + FileExtension
                var newFileName = String.Concat(myUniqueFileName, fileExtension);

                // Combines two strings into a path.
                var filepath =
        new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images")).Root + $@"{newFileName}";

                using (FileStream fs = System.IO.File.Create(filepath))
                {
                    imageFile.CopyTo(fs);
                    fs.Flush();
                }


                post.imagePath = "~/Images/"+newFileName  ;


                postService.CreatePost(post);
                return RedirectToAction(nameof(Index));
            }
            return View(post);
        }

        // GET: posts/Edit/5
        public IActionResult Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = postService.getPostById(id);
            if (post == null)
            {
                return NotFound();
            }
            var list = categoryService.getAllCategories();
            ViewBag.list = new SelectList(list, "categoryID", "name");

            return View(post);
        }

        // POST: posts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("postID,author,title,publishDate,categoryID,tags,description,imagePath")] Post post , IFormFile imageFile)
        {
            if (id != post.postID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var fileName = Path.GetFileName(imageFile.FileName);

                    //Assigning Unique Filename (Guid)
                    var myUniqueFileName = Convert.ToString(Guid.NewGuid());

                    //Getting file Extension
                    var fileExtension = Path.GetExtension(fileName);

                    // concatenating  FileName + FileExtension
                    var newFileName = String.Concat(myUniqueFileName, fileExtension);

                    // Combines two strings into a path.
                    var filepath =
            new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images")).Root + $@"{newFileName}";

                    using (FileStream fs = System.IO.File.Create(filepath))
                    {
                        imageFile.CopyTo(fs);
                        fs.Flush();
                    }


                    post.imagePath = "~/Images/" + newFileName;
                    postService.UpdatePost(post);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (postService.getPostById(id)==null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(post);
        }

        // GET: posts/Delete/5
        public IActionResult Delete(int id)
        {
         
            var post = postService.getPostById(id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // POST: posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
         
            postService.DeletePost(id);

            return RedirectToAction("Index");
        }

    }
}
