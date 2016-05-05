using FreeAD.Infrastructure;
using FreeAD.Infrastructure.Alerts;
using FreeAD.Infrastructure.Filters;
using FreeAD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using FreeADPortable.Domain;
using FreeADPortable.Models;
using AutoMapper.QueryableExtensions;
using FreeAD.Models.ViewModels;

namespace FreeAD.Controllers
{
    [Authorize]
    public class AdvertisementController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ICurrentUser _currentUser;
        public AdvertisementController(ApplicationDbContext context, ICurrentUser currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }
        public ActionResult Index()
        {
            var languageId=HttpContext.GetLanguageId();
            var condition = new ConditionViewModel<Advertisement,AdvertisementViewModel>() {LanguageId=languageId, ChangeOrderDirection = false, OrderDirection = "desc" };
            condition.Func = d => d.Category.LanguageId==languageId && d.Operator==_currentUser.User.UserName && d.Deleted != true;
            var vm = _context.GetSearchResult(condition, d => d.OperationDate, new string[] { "Category" });
            return View(vm);
        }
        [HttpPost]
        public ActionResult Index(ConditionViewModel<Advertisement, AdvertisementViewModel> input)
        {
            Expression<Func<Advertisement, bool>> searchName;
            if (string.IsNullOrEmpty(input.Search))
            {
                searchName = d => d.Category.LanguageId == input.LanguageId && d.Operator == _currentUser.User.UserName && d.Deleted != true;
            }
            else
            {
                searchName = d => d.Category.LanguageId == input.LanguageId && d.Operator == _currentUser.User.UserName && d.Deleted != true && (d.KeyWord.Contains(input.Search));
            }
            input.Func = searchName;
            switch (input.Order)
            {
                case "Category":
                    return View(_context.GetSearchResult(input, d => d.Category.Name, new string[] { "Category" }, false));
                case "KeyWord":
                    return View(_context.GetSearchResult(input, d => d.KeyWord, new string[] { "Category" }, false));
                case "StartDate":
                    return View(_context.GetSearchResult(input, d => d.ValidStartDate, new string[] { "Category" }, false));
                case "EndDate":
                    return View(_context.GetSearchResult(input, d => d.ValidEndDate, new string[] { "Category" }, false));
                case "ShortDescription":
                    return View(_context.GetSearchResult(input, d => d.ShortDescription, new string[] { "Category" }, false));
                default:
                    return View(_context.GetSearchResult(input, d => d.OperationDate, new string[] { "Category" }, false));
            }
        }
        [ValidateInput(false)]
        [Transcation]
        public ActionResult AddOrEdit(AdvertisementViewModel input)
        {
            try
            {
                //throw new Exception();
                input.OperationDate = _currentUser.NewZealandTime;
                var detailMessage = "";
                if (!string.IsNullOrEmpty(input.Detail))
                {
                    if (input.Detail.Substring(input.Detail.Length - 1) != ">")
                    {
                        detailMessage = "System didn't save your detail info. It is because the detail size is over chrome limit. Please try using IE or decrease your data like picture size.";
                    }
                }
                var entity = AutoMapper.Mapper.Map<Advertisement>(input);

                if (string.IsNullOrEmpty(input.Operator))
                {
                    entity.Operator = _currentUser.User.UserName;
                    _context.Advertisements.Add(entity);
                }
                else
                {
                    if (input.Detail.Substring(input.Detail.Length - 1) != ">")
                    {
                        input.Detail = _context.Advertisements.Find(input.Id).Detail;
                    }
                    if (input.Operator != _currentUser.User.UserName)
                        return RedirectToAction("Index").WithError("Failure, you are not supposed to change other's data.");
                    var existing = _context.Advertisements.Find(input.Id);
                    _context.Entry(existing).CurrentValues.SetValues(entity);
                }
                _context.SaveChanges();
                if(!string.IsNullOrEmpty(detailMessage))
                    return RedirectToAction("Index").WithSuccess("Success, your data saved." + detailMessage);
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                HttpContext.Items["Error"] = true;
                return RedirectToAction("Index").WithError("Failure, there are something wrong, please check your input.");
            }
        }
        public JsonResult _Delete(long id)
        {
            try
            {
                _context.Advertisements.Find(id).Deleted = true;
                _context.SaveChanges();
                return Json("success", JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                var m = e.Message;
                return Json("fail", JsonRequestBehavior.AllowGet);
            }
        }
        public PartialViewResult OpenAddOrEdit(long id)
        {
            var ad = _context.Advertisements.Include("Category").Where(d=>d.Id==id).ProjectTo<AdvertisementViewModel>().First();
            return PartialView("_AddOrEdit", ad);
        }
    }
}