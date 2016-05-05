using FreeAD.Infrastructure;
using FreeAD.Models;
using FreeAD.Models.ViewModels;
using FreeADPortable.Domain;
using FreeADPortable.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace FreeAD.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext _db;
        public HomeController(ApplicationDbContext db)
        {
            _db = db;
        }
        public ActionResult Index(string id)
        {
            if (id != null)
                ViewBag.Id = id;
            return View(new ConditionViewModel<Advertisement,AdvertisementViewModel>());
        }
        [HttpPost]
        public ActionResult Index(ConditionViewModel<Advertisement, AdvertisementViewModel> input)
        {
            input.Search = input.Search ?? "";
            Expression<Func<Advertisement, bool>> searchName;
            
                long categoryId = long.Parse(input.SearchTwo);
            var category = _db.Categories.Find(categoryId);

                if (category.Order==0)
                    searchName = t => t.Category.LanguageId==category.LanguageId && t.KeyWord.Contains(input.Search) && t.Deleted != true;
                else
                    searchName = t => t.CategoryId== categoryId && t.KeyWord.Contains(input.Search) && t.Deleted != true;

            input.Func = searchName;
            switch (input.Order)
            {
                case "Date":
                    return View(_db.GetSearchResult(input, d => d.ValidStartDate, new string[] { "category"}, false));
                case "Area":
                    return View(_db.GetSearchResult(input, d => d.Area, new string[] { "category" }, false));
                case "Popular":
                    return View(_db.GetSearchResult(input, d => d.Popular, new string[] { "category" }, false));
                default:
                    return View(_db.GetSearchResult(input, d => d.Id, new string[] { "category" }, false));
            }
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        private long getLanguageId()
        {
            var languageId = 1;
            if (Request.Cookies["Language"] != null)
            {
                languageId = int.Parse(Request.Cookies["Language"].Values["Id"].ToString().Trim());
            }
            if (!_db.Get<Language>(d => d.Id == languageId).Any())
            {
                languageId = 1;
            }
            return languageId;
        }
        public ActionResult ChangeLanguage(long LanguageId)
        {
            if (Request.Cookies["Language"] != null)
            {
                Response.Cookies.Remove("Language");
            }
            var cookie = new HttpCookie("Language");
            cookie.Values.Add("Id", LanguageId.ToString());
            cookie.Expires = DateTime.Now.AddDays(15);
            Response.Cookies.Add(cookie);
            return RedirectToAction("Index");
        }

        public ActionResult ModalDetail(long id)
        {
            var ad=_db.Advertisements.Include("Category").FirstOrDefault(d=>d.Id==id);
            return PartialView("_ModalFullDetail",ad);
        }
        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        _db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}