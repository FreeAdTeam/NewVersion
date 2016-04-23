using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace FreeADPortable.Models
{
    public class ConditionViewModel<T> where T : class
    {
        public ConditionViewModel()
        {
            ApiData = new List<object>();
            Data = new List<T>();
        }
        public int CurrentPage { get; set; } = 1;
        public int PerPageSize { get; set; } = 10;
        public int TotalPages { get; set; }
        public int Status { get; set; }
        public string Search { get; set; }
        public string SearchTwo { get; set; }
        public string SearchThree { get; set; }
        public string Order { get; set; }
        public int LanguageId { get; set; }
        public Boolean ChangeOrderDirection { get; set; }
        public string OrderDirection { get; set; }
        public List<T> Data { get; set; }
        public IEnumerable<object> ApiData { get; set; }
        public Expression<Func<T, bool>> Func { get; set; }
    }
}