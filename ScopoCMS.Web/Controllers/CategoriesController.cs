using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ScopoCMS.Web.Data;
using ScopoCMS.Web.Models;
using ScopoCMS.Web.Services;

namespace ScopoCMS.Web.Controllers
{
    public class CategoriesController : Controller
    {
        public CategoryService categoryService;
 

        public CategoriesController(CategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {
            var categories = categoryService.getAllCategories();
            return View(categories);
        }

        // GET: Categories/Details/5
       

        // GET: Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("categoryID,name")] Category category)
        {
            if (ModelState.IsValid)
            {
                categoryService.Create(category);
                
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            
            //if (category == null)
            //{
            //    return NotFound();
            //}
            return View();
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("categoryID,name")] Category category)
        {
            if (id != category.categoryID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    categoryService.Update(category);
                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    //if (!CategoryExists(category.categoryID))
                    //{
                    //    return NotFound();
                    //}
                    //else
                    //{
                    //    throw;
                    //}
                }
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            categoryService.Delete(id);
            var category = categoryService.getCategoryByID(id);
              
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //var category = await _context.categories.FindAsync(id);
            //_context.categories.Remove(category);
            //await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //private bool CategoryExists(int id)
        //{
        //    return _context.categories.Any(e => e.categoryID == id);
        //}
    }
}
