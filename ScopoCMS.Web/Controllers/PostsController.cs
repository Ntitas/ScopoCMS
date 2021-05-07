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
            ViewBag.categories = new SelectList(_context.Categories, "CategoryID", "Name");
            var imgpath = Path.Combine(_appEnvironment.WebRootPath, "Images");
            string[] filePaths = Directory.GetFiles(imgpath);
            List<string> p = new List<string>();

            foreach (var item in filePaths)
            {
                var itemPath = Path.GetFileName(item);
                var url = "/Images/" + itemPath;
                p.Add(url);

            }
            ViewBag.paths = p;



            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Post post)
        {
            if (post.ImagePath == null)
            {
                post.ImagePath = "~/Images/noimage.png";
                post.ShortDesc = post.Description.Substring(0, 150); ;

                if (ModelState.IsValid)
                {
                    _context.Add(post);
                    await _context.SaveChangesAsync();
                }
                var imgpath = Path.Combine(_appEnvironment.WebRootPath, "Images");
                string[] filePaths = Directory.GetFiles(imgpath);
                List<string> p = new List<string>();

                foreach (var item in filePaths)
                {
                    var itemPath = Path.GetFileName(item);
                    var url = "/Images/" + itemPath;
                    p.Add(url);

                }
                ViewBag.paths = p;


                ViewBag.categories = new SelectList(_context.Categories, "CategoryID", "Name", post.CategoryID);
                return View(post);
            }
            else
            {

                post.ShortDesc = post.Description.Substring(0, 150);

                if (ModelState.IsValid)
                {
                    _context.Add(post);
                    await _context.SaveChangesAsync();
                }
                var imgpath = Path.Combine(_appEnvironment.WebRootPath, "Images");
                string[] filePaths = Directory.GetFiles(imgpath);
                List<string> p = new List<string>();

                foreach (var item in filePaths)
                {
                    var itemPath = Path.GetFileName(item);
                    var url = "/Images/" + itemPath;
                    p.Add(url);

                }
                ViewBag.paths = p;


                ViewBag.categories = new SelectList(_context.Categories, "CategoryID", "Name", post.CategoryID);
                return RedirectToAction("Index", "Posts");
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
            var imgpath = Path.Combine(_appEnvironment.WebRootPath, "Images");
            string[] filePaths = Directory.GetFiles(imgpath);
            List<string> p = new List<string>();

            foreach (var item in filePaths)
            {
                var itemPath = Path.GetFileName(item);
                var url = "/Images/" + itemPath;
                p.Add(url);

            }
            ViewBag.paths = p;
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

            if (post.ImagePath == null)
            {
                post.ImagePath = "~/Images/noimage.png";
                post.ShortDesc = post.Description.Substring(0, 150); ;

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
                post.ShortDesc = post.Description.Substring(0, 150);

                if (ModelState.IsValid)
                {
                    _context.Update(post);
                    await _context.SaveChangesAsync();
                }
                ViewBag.categories = new SelectList(_context.Categories, "CategoryID", "Name", post.CategoryID);
                var imgpath = Path.Combine(_appEnvironment.WebRootPath, "Images");
                string[] filePaths = Directory.GetFiles(imgpath);
                List<string> p = new List<string>();

                foreach (var item in filePaths)
                {
                    var itemPath = Path.GetFileName(item);
                    var url = "/Images/" + itemPath;
                    p.Add(url);

                }
                ViewBag.paths = p;
                return RedirectToAction("Index","Posts");
            }

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
