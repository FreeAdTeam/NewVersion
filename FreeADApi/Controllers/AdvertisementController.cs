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
    public class AdvertisementController : ApiController
    {
        ApplicationDbContext _db ;
        const int maxPageSize = 10;
        public AdvertisementController()
        {
            _db = new ApplicationDbContext();
        }
        [Route("api/category/{categoryId}/advertisements", Name = "advertisementList")]
        public IHttpActionResult Get(int categoryId = 0, string search = "none", string sort = "id",string includes=null, string fields = null, int page = 1, int pageSize = maxPageSize)
        {
            try
            {
                var Con = new ConditionViewModel<Advertisement>();
                Con.Func = d => d.Category.LanguageId==1 && (categoryId==0 || d.CategoryId==categoryId) &&(search=="none"||d.KeyWord.Contains(search)) && d.Deleted==false;
                Con.CurrentPage = page;
                Con.PerPageSize = pageSize;
                var result=_db.GetSearchResult(Con,sort,includes,fields);

                var urlHelper = new UrlHelper(Request);
                var prevLink = result.CurrentPage > 1 ? urlHelper.Link("advertisementList",
                    new
                    {
                        page = result.CurrentPage - 1,
                        pageSize = result.PerPageSize,
                        sort = sort,
                        fields = fields,
                        categoryId = categoryId
                    }) : "";
                var nextLink = result.CurrentPage < result.TotalPages ? urlHelper.Link("advertisementList",
                    new
                    {
                        page = result.CurrentPage + 1,
                        pageSize = result.PerPageSize,
                        sort = sort,
                        fields = fields,
                        categoryId = categoryId
                    }) : "";


                var paginationHeader = new
                {
                    currentPage = result.CurrentPage,
                    pageSize = result.PerPageSize,
                    totalPages = result.TotalPages,
                    previousPageLink = prevLink,
                    nextPageLink = nextLink
                };
                HttpContext.Current.Response.Headers.Add("X-Pagination",Newtonsoft.Json.JsonConvert.SerializeObject(paginationHeader));
                // return result
                return Ok(result.ApiData);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }
        public IHttpActionResult Get(int id)
        {
            try
            {
                var ad=_db.Set<Advertisement>().Find(id);
                return Ok(ad);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }
    }
}
