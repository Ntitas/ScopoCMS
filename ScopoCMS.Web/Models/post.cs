using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScopoCMS.Web.Models
{
    public class Post
    {
        public int postID { get; set; }
        public string author { get; set; }
        public string title { get; set; }

        public DateTime publishDate { get; set; }
        public string category { get; set; }

        public string tags { get; set; }

        public string description { get; set; }

        public byte[] image { get; set; }

        


    }
}
