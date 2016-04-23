using FreeAD.Infrastructure;
using FreeADPortable.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FreeAD.Controllers
{
    public class PoneAdvertisementController : Controller
    {
        ApplicationDbContext _db;
        public PoneAdvertisementController(ApplicationDbContext db)
        {
            _db = db;
        }
        public ActionResult Index(int id)
        {
            var ad=_db.Get<Advertisement>(d => d.Id == id).First();
            return View(ad);
        }
        public ActionResult Manual(int languageId = 1)
        {
            return View();
        }
        public ActionResult Contact(int languageId=1)
        {
            return View();
        }
    }
}