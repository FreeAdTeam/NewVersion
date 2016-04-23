using FreeADApi.Models;
using FreeADPortable.Domain;
using FreeADPortable.Helpers;
using FreeADPortable.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Routing;

namespace FreeADApi.Controllers
{
    public class CategoryController : ApiController
    {
        ApplicationDbContext _db;
        const int maxPageSize = 10;
        public CategoryController()
        {
            _db = new ApplicationDbContext();
        }
        [Route("api/category")]
        public IHttpActionResult Get(string sort = "id", string includes = null, string fields = null, string search = "1", int page = 1, int pageSize = maxPageSize)
        {
            try
            {
                int languageId= int.Parse(search); ;
                var Con = new ConditionViewModel<Category>();
                Con.Func = d => d.LanguageId == languageId;
                Con.CurrentPage = page;
                Con.PerPageSize = pageSize;
                var result = _db.GetSearchResult(Con, sort, includes, fields);
                return Ok(result.ApiData);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }
    }
}
