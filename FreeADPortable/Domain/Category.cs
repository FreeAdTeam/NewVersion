using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FreeADPortable.Domain
{
    public class Category
    {
        public Category()
        {
            this.CategoryChildren = new HashSet<Category>();
            this.Advertisements = new HashSet<Advertisement>();
        }
        public long Id { get; set; }
        public string Name { get; set; }
        public string ColorClass { get; set; }
        public int? Order { get; set; }
        [ForeignKey("LanguageId")]
        public Language Language { get; set; }
        public long LanguageId { get; set; }
        [ForeignKey("ParentId")]
        public virtual Category CategoryParent { get; set; }
        public long? ParentId { get; set; }
        public virtual ICollection<Category> CategoryChildren { get; set; }
        public virtual ICollection<Advertisement> Advertisements { get; set; }
    }
}