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
        public void CreateSectionTPost(Section section)
        {
            PostSection postSection = new PostSection();
            postSection.SectionID = section.sectionId;
            postSection.section = section;
            foreach(var i in section.Posts)
            {
                postSection.PostID = i.postID;
                postSection.post = dbContext.posts.Find(i.postID);
                dbContext.Add(postSection);
                dbContext.SaveChanges();

            }
       
            return;
        }
        public void Update(Section section)
        {
            if (dbContext.sections.Find(section.sectionId) != null)
            {
                dbContext.Update(section);
                dbContext.SaveChanges();
            }

        }
        public IEnumerable<Section> getAllSection()
        {
            var res = dbContext.sections.ToList();
            return res;
        }
        public Section getSectionById(int id)
        {
            var res = dbContext.sections.Find(id);
            return res;
        }
        public void UpdateSection(Section section)
        {
            dbContext.Update(section);
            dbContext.SaveChanges();
        }
        public IEnumerable<PostSection> getPostinSection()
        {
            var res = dbContext.postSections.ToList();

            return res;
        }
    }
}
