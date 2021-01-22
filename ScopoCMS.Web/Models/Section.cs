﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScopoCMS.Web.Models
{
    public class Section
    {
        public int sectionId { get; set; }
        public string name { get; set; }
        public Section()
        {
            Posts = new List<Post>();
        }

        public virtual ICollection<Post> Posts { get; set; }
    }
}
