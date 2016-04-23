using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FreeADPortable.Domain
{
    public class Language
    {
        public Language()
        {
            Categories = new HashSet<Category>();
        }
        public long Id { get; set; }
        public string Name { get; set; }
        public string profile { get; set; }
       
        public virtual ICollection<Category> Categories { get; set; }
    }
}