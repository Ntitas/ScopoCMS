using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ScopoCMS.Web.Data;
using ScopoCMS.Web.Models;
using ScopoCMS.Web.ViewModels;

namespace ScopoCMS.Web.Services
{
    public class SectionService
    {

        private readonly CMSDbContext dbContext;
        public SectionService(CMSDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Create(SectionCreateViewModel scvm)
        {
            Section section = new Section();
            section.sectionId = scvm.sectionId;
            section.name = scvm.name;
            dbContext.Add(section);
            dbContext.SaveChanges();
        }
        public void CreateSectionTPost(SectionCreateViewModel scvm)
        {
        
            

            foreach (var i in scvm.postId)
            {
                PostSection postSection = new PostSection();
                var sec = (from s in dbContext.sections
                          where s.name==scvm.name select new Section { 
                          sectionId=s.sectionId,
                          name=s.name}).FirstOrDefault();
                postSection.SectionID = sec.sectionId;
                postSection.PostID = i;
                dbContext.Add(postSection);
                dbContext.SaveChanges();

            }

            return;
        }
        public void UpdateSectionContent(SectionCreateViewModel scvm)
        {
            var res = (from ps in dbContext.PostSection where ps.SectionID == scvm.sectionId
                       select ps).ToList();
            foreach(var item in res)
            {
                dbContext.Remove(item);

            }
            dbContext.SaveChanges();
            Section section = new Section();
            section.sectionId = scvm.sectionId;
            section.name = scvm.name;
            dbContext.sections.Update(section);
            dbContext.SaveChanges();
            
            foreach(var i in scvm.postId)
            {
                PostSection postSection = new PostSection();
                postSection.PostID = i;
                postSection.SectionID = scvm.sectionId;
                dbContext.Update(postSection);
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
        public IEnumerable<PostViewModel> getPostinSection()
        {
            var res = (from ps in dbContext.PostSection
                       join p in dbContext.posts on ps.PostID equals p.postID
                       join s in dbContext.sections on ps.SectionID equals s.sectionId
                       select new PostViewModel
                       {
                           postID=ps.PostID,
                           author=p.author,
                           title=p.title,
                           publishDate=p.publishDate,
                           categoryID=p.categoryID,
                           tags=p.tags,
                           description=p.description,
                           imagePath=p.imagePath,
                           sectionId=s.sectionId,
                           sectionname=s.name

                       }).AsEnumerable();
            return res;
        }
        public IEnumerable<PostViewModel> getPostinSectionById(int id)
        {
            var res = (from ps in dbContext.PostSection
                       join p in dbContext.posts on ps.PostID equals p.postID
                       join s in dbContext.sections on ps.SectionID equals s.sectionId
                       where s.sectionId==id
                       select new PostViewModel
                       {
                           postID = ps.PostID,
                           author = p.author,
                           title = p.title,
                           publishDate = p.publishDate,
                           categoryID = p.categoryID,
                           tags = p.tags,
                           description = p.description,
                           imagePath = p.imagePath,
                           sectionId = s.sectionId,
                           sectionname = s.name

                       }).AsEnumerable();
            return res;
        }
        public IEnumerable<PostViewModel> getPostinSectionByName(string name)
        {
            var res = (from ps in dbContext.PostSection
                       join p in dbContext.posts on ps.PostID equals p.postID
                       join s in dbContext.sections on ps.SectionID equals s.sectionId
                       where s.name == name
                       select new PostViewModel
                       {
                           postID = ps.PostID,
                           author = p.author,
                           title = p.title,
                           publishDate = p.publishDate,
                           categoryID = p.categoryID,
                           tags = p.tags,
                           description = p.description,
                           imagePath = p.imagePath,
                           sectionId = s.sectionId,
                           sectionname = s.name

                       }).AsEnumerable();
            return res;
        }
    }
}
