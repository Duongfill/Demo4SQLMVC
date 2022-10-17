using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Demo4SQLMVC.Models;
using System.IO;

namespace Demo4SQLMVC.Areas.Admin.Controllers
{
    public class SANPHAMsController : Controller
    {
        private BTL_MVCEntities db = new BTL_MVCEntities();

        // GET: Admin/SANPHAMs
        public ActionResult Index()
        {
            return View(db.SANPHAMs.ToList());
        }
        public ActionResult getAllData()
        {
            return Json(db.SANPHAMs.Select(s => new { s.MASP, s.TENSP, s.ANH, s.HANGSP,s.SOLUONG,s.GIASPB }).ToList(), JsonRequestBehavior.AllowGet);
        }
        public ActionResult inputData()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult inputData([Bind(Include = "MASP,TENSP,ANH,HANGSP,SOLUONG,GIASPB")] SANPHAM product, HttpPostedFileBase Image)
        {

            //string _Path ="";
            //string fileName = "";

            //if (Image.ContentLength > 0)
            //{
            //    fileName =Path.GetDirectoryName(Image.FileName);
            //    _Path =Path.Combine(Server.MapPath("~/Uploadimg/"), fileName);
            //    Image.SaveAs(_Path);
            //}
            //product.ANH = "~/Uploadimg/" + fileName;
            product.SOLUONG = product.SOLUONG;
            product.GIASPB = product.GIASPB;
            if (ModelState.IsValid)
            {
                

                try
                {
                    db.SANPHAMs.Add(product);
                    db.SaveChanges();
                    return Json(new { msg = true }, JsonRequestBehavior.AllowGet);
                }
                catch
                {
                    return Json(new { msg = false }, JsonRequestBehavior.AllowGet);
                }

            }

            return Json(new { msg = false }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult viewData()
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

        // GET: Admin/SANPHAMs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SANPHAM sANPHAM = db.SANPHAMs.Find(id);
            if (sANPHAM == null)
            {
                return HttpNotFound();
            }
            return View(sANPHAM);
        }

        // GET: Admin/SANPHAMs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/SANPHAMs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create([Bind(Include = "MASP,TENSP,HANGSP,SOLUONG,ANH,GIASPB")] SANPHAM sANPHAM ,HttpPostedFileBase Image)
        {
            string _Path ="" /*Server.MapPath("~/Uploadimg/")*/ ;
            string fileName = "";
            try
            {
                if (Image.ContentLength > 0)
                {
                    fileName = Path.GetDirectoryName(Image.FileName);

                    _Path = Path.Combine(Server.MapPath("~/Uploadimg/"), fileName);
                    Image.SaveAs(_Path);
                }
            }
            catch { }

            if (ModelState.IsValid)
            {
                sANPHAM.ANH = "~/Uploadimg/" + fileName;
                sANPHAM.ANH = fileName;
                db.SANPHAMs.Add(sANPHAM);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sANPHAM);
        }

        // GET: Admin/SANPHAMs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SANPHAM sANPHAM = db.SANPHAMs.Find(id);
            if (sANPHAM == null)
            {
                return HttpNotFound();
            }
            return View(sANPHAM);
        }

        // POST: Admin/SANPHAMs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MASP,TENSP,HANGSP,SOLUONG,ANH,GIASPB")] SANPHAM sANPHAM, HttpPostedFileBase Image)
        {


            if (ModelState.IsValid)
            {
                //SANPHAM modifysanpham = db.SANPHAMs.Find(sANPHAM.MASP);
                //string _Path = Server.MapPath("~/Uploadimg/") + "/";
                //string fileName = "";
                //if (modifysanpham != null)
                //{
                //    if (Image != null && Image.ContentLength > 0)
                //    {
                //        fileName = System.IO.Path.GetFileName(Image.FileName);
                //        _Path = System.IO.Path.Combine(Server.MapPath("~/Uploadimg/"), fileName);
                //        Image.SaveAs(_Path);
                //    }
                //}
                //modifysanpham.ANH = fileName;
                
                db.Entry(sANPHAM).State = EntityState.Modified;
                
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sANPHAM);
        }

        // GET: Admin/SANPHAMs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SANPHAM sANPHAM = db.SANPHAMs.Find(id);
            db.SANPHAMs.Remove(sANPHAM);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: Admin/SANPHAMs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SANPHAM sANPHAM = db.SANPHAMs.Find(id);
            db.SANPHAMs.Remove(sANPHAM);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
















