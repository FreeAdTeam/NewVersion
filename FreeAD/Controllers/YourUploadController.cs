using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using FreeAD.Infrastructure.Filters;
using FreeAD.Infrastructure;
using FreeADPortable.Domain;

namespace FreeAD.Controllers
{
    public class YourUploadController : Controller
    {
        ApplicationDbContext _db;
        ICurrentUser _currentUser;
        public YourUploadController(ApplicationDbContext db, ICurrentUser currentUser)
        {
            _db = db;
            _currentUser = currentUser;
        }
        public ActionResult Index()
        {
            return View(_db.UserImages.Where(d=>d.Operator==_currentUser.User.Email && d.Deleted!=true).OrderByDescending(d=>d.Id));
        }
        [HttpPost,Transcation]
        public ActionResult Upload()
        {
            for (int i = 0; i < Request.Files.Count; i++)
            {
                var file = Request.Files[i];
                var fileName = Path.GetFileName(file.FileName);
                if (string.IsNullOrEmpty(fileName)) return RedirectToAction("Index");
                var ext = fileName.Substring(fileName.IndexOf(".")).ToLower();

                if (ext == ".jpg" || ext == ".png" || ext == ".bmp")
                {
                    if (file.ContentLength > 1000000) continue;
                }

                var newImage = new UserImage
                {
                    OperationDate =_currentUser.NewZealandTime,
                    Operator =_currentUser.User.Email,
                    FileType =ext,
                    FileName=fileName
                };
                _db.UserImages.Add(newImage);
                _db.SaveChanges();
                file.SaveAs(Server.MapPath("~/UserUpload/" + newImage.Id+ext));
            }
            return RedirectToAction("Index");
        }
        public JsonResult _Delete(long id)
        {
            try
            {
                _db.UserImages.Find(id).Deleted = true;
                _db.SaveChanges();
                return Json("success", JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json("fail", JsonRequestBehavior.AllowGet);
            }
        }
    }
}