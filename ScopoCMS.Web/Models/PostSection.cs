using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ScopoCMS.Web.Models
{
    public class PostSection
    {
        [Key]
        public int PostSectionID { get; set; }
       
        public int PostID { get; set; }
       

        
        public int SectionID { get; set; }
  


    }
}
