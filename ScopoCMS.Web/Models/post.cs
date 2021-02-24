using System;

namespace ScopoCMS.Web.Models
{
    public class Post
    {
        public int PostID { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ShortDesc { get; set; }
        public string ImagePath { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime PublishDate { get; set; }
        public string Tags { get; set; }

        public int CategoryID { get; set; }
        public virtual Category Category { get; set; }
    }
}
