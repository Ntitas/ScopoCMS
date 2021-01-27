using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ScopoCMS.Web.Data;
using ScopoCMS.Web.Models;

namespace ScopoCMS.Web.Services
{
    public class SectionService
    {

        private readonly CMSDbContext dbContext;
        public SectionService(CMSDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Create( Section section)
        {
            dbContext.Add(section);
            dbContext.SaveChanges();
        }
        public void Update(Section section)
        {
            dbContext.Update(section);
            dbContext.SaveChanges();
        }
       
    }
}
