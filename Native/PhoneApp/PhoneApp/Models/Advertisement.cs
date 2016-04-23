using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite.Net.Attributes;
namespace PhoneApp.Models
{
    public class Advertisement
    {
		[PrimaryKey]
        public int Id { get; set; }
		public int CategoryId{ get; set;}
        public string Keyword { get; set; }
        public string ShortDescription { get; set; }
    }
}
