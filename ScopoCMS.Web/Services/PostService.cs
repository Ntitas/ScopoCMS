using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ScopoCMS.Web.Data;
using ScopoCMS.Web.Models;

namespace ScopoCMS.Web.Services
{
    public class PostService
    {
        private readonly CMSDbContext dbContext;

        public PostService( CMSDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<Post> getAllPosts()
        {
            var res = dbContext.posts.ToList();
            return res;
        }
        public Post getPostById(int id)
        {
            var res = dbContext.posts.Find(id);
            return res;
        }
        public void CreatePost(Post post)
        {
            dbContext.Add(post);
            dbContext.SaveChanges();
        }
        public void UpdatePost(Post post)
        {
            dbContext.Update(post);
            dbContext.SaveChanges();
        }
        public void DeletePost(int id)

        {
            
            dbContext.Remove(dbContext.posts.Find(id));
            dbContext.SaveChanges();
        }
    }
}
