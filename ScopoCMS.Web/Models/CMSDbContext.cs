using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScopoCMS.Web.Models
{
    public class CMSDbContext : DbContext
    {
        public DbSet<Post> posts { get; set; }
        public DbSet<Category> categories { get; set; }
        public DbSet<Section> sections { get; set; }
    }
}
