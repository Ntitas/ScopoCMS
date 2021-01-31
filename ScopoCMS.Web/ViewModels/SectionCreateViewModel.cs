using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScopoCMS.Web.ViewModels
{
    public class SectionCreateViewModel
    {
        public int sectionId { get; set; }
        public string name { get; set; }

        public int[] postId { get; set; }
    }
}
