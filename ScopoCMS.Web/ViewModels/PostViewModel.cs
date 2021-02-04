using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScopoCMS.Web.ViewModels
{
    public class PostViewModel
    {
        public int PostID { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime PublishDate { get; set; }
        public string Tags { get; set; }

        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
    }
}
