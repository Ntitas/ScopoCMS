using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ScopoCMS.Web.Models
{
    [Keyless]
    public class PostSection
    {
        //[Key]
        //[Column(Order = 0)]

   
        public int PostID { get; set; }
        //[Key]
        //[Column(Order =1)]
        public int SectionID { get; set; }
     
        public virtual Post post { get; set; }
    
        public virtual Section section { get; set; }
    }
}
