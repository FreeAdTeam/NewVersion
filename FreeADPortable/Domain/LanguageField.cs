using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FreeADPortable.Domain
{
    public class LanguageField
    {
        public long Id { get; set; }
        public string EnglishName { get; set; }
        public string ShowName { get; set; }
        [ForeignKey("LanguageId")]
        public Language Language { get; set; }
        public long LanguageId { get; set; }
    }
}