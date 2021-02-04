using Microsoft.EntityFrameworkCore;

namespace ScopoCMS.Web.Models
{
    public class CMSDbContext : DbContext
    {
        public CMSDbContext(DbContextOptions<CMSDbContext> options)
           : base(options)
        {
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
