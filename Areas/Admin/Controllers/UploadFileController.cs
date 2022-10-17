using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace Demo4SQLMVC.Areas.Admin.Controllers
{
    public class UploadFileController : Controller
    {
        // GET: Admin/UploadFile
        public ActionResult Index()
        {
            return View();
        }
        public ContentResult Upload()
        {
            string path = Server.MapPath("~/Uploadimg/");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }


            foreach (string key in Request.Files)
            {
                HttpPostedFileBase postedFile = Request.Files[key];
                postedFile.SaveAs(path + postedFile.FileName);
            }

            return Content("Success");
        }
        public ActionResult testck()
        {
            return View();
        }
    }
}