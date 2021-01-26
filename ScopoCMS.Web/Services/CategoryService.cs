using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ScopoCMS.Web.Data;
using ScopoCMS.Web.Models;

namespace ScopoCMS.Web.Services
{
    public class CategoryService
    {
        private readonly ApplicationDbContext dbContext;
        public CategoryService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
         public void Create( Category category)
        {
            dbContext.Add(category);
            dbContext.SaveChanges();
        }
        public IEnumerable<Category> getAllCategories()
        {
            var res = dbContext.categories.AsEnumerable();
            return res;
         
        }

        public Category getCategoryByID(int id)
        {
            var res = dbContext.categories.Find(id);
            return res;
        }

        public void Update(Category category)
        {
            dbContext.Update(category);
            dbContext.SaveChanges();
        }
        public void Delete(int? id)
        {
            var category=dbContext.categories.Find(id);
            dbContext.Remove(category);
            dbContext.SaveChanges();
        }
    }
}
