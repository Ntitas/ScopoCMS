using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScopoCMS.Web.Models
{
    public class Category
    {
        public int CategoryID { get; set; }
        public string Name { get; set; }
        public ICollection<Post> Posts { get; set; }
    }
}
