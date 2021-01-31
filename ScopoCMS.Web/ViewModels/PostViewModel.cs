using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScopoCMS.Web.ViewModels
{
    public class PostViewModel
    {
        public int postID { get; set; }
        public string author { get; set; }
        public string title { get; set; }

        public DateTime publishDate { get; set; }
        public int categoryID { get; set; }

        public string tags { get; set; }

        public string description { get; set; }

        public string imagePath { get; set; }
        public int sectionId { get; set; }
        public string  sectionname { get; set; }

    }
}
