using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ScopoCMS.Web.Models;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;

namespace ScopoCMS.Web.Controllers
{
    public class PostsController : Controller
    {
        private readonly CMSDbContext _context;
        private readonly IWebHostEnvironment _appEnvironment;

        public PostsController(CMSDbContext context, IWebHostEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
        }

        // GET: Posts
        public async Task<IActionResult> Index()
        {
            var cMSDbContext = _context.Posts.Include(p => p.Category);
           
            return View(await cMSDbContext.ToListAsync());
        }

        public async Task<IActionResult> ShowPostsByCategory(int id)
        {
            var cMSDbContext = _context.Posts.Where(p=>p.CategoryID==id).Include(p=>p.Category).ToListAsync();
            ViewData["CatList"] = await _context.Categories.ToListAsync();

            return View(await cMSDbContext);
        }
        public async Task<IActionResult> PostDetails(int id)
        {
            var cMSDbContext = _context.Posts.Where(p => p.PostID == id).Include(p => p.Category).FirstOrDefaultAsync();

            ViewData["CatList"] = await _context.Categories.ToListAsync();
            return View(await cMSDbContext);
        }



        // GET: Posts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.PostID == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // GET: Posts/Create
        public IActionResult Create()
        {
            ViewBag.categories= new SelectList(_context.Categories, "CategoryID", "Name");
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Post post, IFormFile image)
        {
            if(image==null)
            {
                post.ImagePath = "~/Images/noimage.png";
                post.ShortDesc= post.Description.Substring(0, 150); ;

                if (ModelState.IsValid)
                {
                    _context.Add(post);
                    await _context.SaveChangesAsync();
                }

                ViewBag.categories = new SelectList(_context.Categories, "CategoryID", "Name", post.CategoryID);
                return View(post);
            }
            else
            { 
            var imgpath = Path.Combine(_appEnvironment.WebRootPath, "Images");
            var fileName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(image.FileName);
            using (var fileStream = new FileStream(Path.Combine(imgpath, fileName), FileMode.Create))
            {
                await image.CopyToAsync(fileStream);
                string filePath = "uploads\\img\\" + fileName;
                post.ImagePath = "~/Images/" + fileName;
                post.ShortDesc = post.Description.Substring(0, 150);

                if (ModelState.IsValid)
                {
                    _context.Add(post);
                    await _context.SaveChangesAsync();
                }

                ViewBag.categories = new SelectList(_context.Categories, "CategoryID", "Name", post.CategoryID);
                return View(post);
            }
        }
        }

            // GET: Posts/Edit/5
            public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            ViewBag.categories = new SelectList(_context.Categories, "CategoryID", "Name", post.CategoryID);
            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Post post)
        {
            if (id != post.PostID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    post.ShortDesc = post.Description.Substring(0, 150);
                    _context.Update(post);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(post.PostID))
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
            ViewBag.categories = new SelectList(_context.Categories, "CategoryID", "Name", post.CategoryID);
            return View(post);
        }

        // GET: Posts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.PostID == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostExists(int id)
        {
            return _context.Posts.Any(e => e.PostID == id);
        }

        public async Task<IActionResult> ManagePost()
        {
            var cMSDbContext = _context.Posts.Include(p => p.Category);
            return View(await cMSDbContext.ToListAsync());
        }








    }
}
