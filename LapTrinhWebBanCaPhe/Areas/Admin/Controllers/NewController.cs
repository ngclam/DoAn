using LapTrinhWebBanCaPhe.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LapTrinhWebBanCaPhe.Areas.Admin.Controllers
{
    public class NewController : Controller
    {
        CAFESHOP_DBModel objCAFESHOP_DBModel = new CAFESHOP_DBModel();
        // GET: Admin/New
        public ActionResult IndexNew()
        {
            var listNew = objCAFESHOP_DBModel.News.ToList();
            return View(listNew);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(News objNew)
        {
                if (objNew.ImageUpload != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(objNew.ImageUpload.FileName);
                    string extension = Path.GetExtension(objNew.ImageUpload.FileName);
                    fileName = fileName + "_" + long.Parse(DateTime.Now.ToString("yyyyMMddhhmmss")) + extension;
                    objNew.NewsImage = fileName;
                    objNew.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/img/blog/"),fileName));
                }
                objCAFESHOP_DBModel.News.Add(objNew);
                objCAFESHOP_DBModel.SaveChanges();
                return RedirectToAction("IndexNew");
        }
        [HttpGet]
        public ActionResult Details(int id)
        {
            var objNew = objCAFESHOP_DBModel.News.Where(n => n.NewsId == id).FirstOrDefault();
            return View(objNew);
        }
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            var objNew = objCAFESHOP_DBModel.News.Where(n => n.NewsId == id).FirstOrDefault();
            return View(objNew);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var objNew = objCAFESHOP_DBModel.News.Where(n => n.NewsId == id).FirstOrDefault();

            objCAFESHOP_DBModel.News.Remove(objNew);
            objCAFESHOP_DBModel.SaveChanges();
            return RedirectToAction("IndexNew");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var objNew = objCAFESHOP_DBModel.News.Where(n => n.NewsId == id).FirstOrDefault();
            return View(objNew);
        }
        [HttpPost]
        public ActionResult Edit(News objNew)
        {
            if (objNew.ImageUpload != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(objNew.ImageUpload.FileName);
                string extension = Path.GetExtension(objNew.ImageUpload.FileName);
                fileName = fileName + "_" + long.Parse(DateTime.Now.ToString("yyyyMMddhhmmss")) + extension;
                objNew.NewsImage = fileName;
                objNew.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/img/blog"), fileName));
            }
            objCAFESHOP_DBModel.Entry(objNew).State = EntityState.Modified;
            objCAFESHOP_DBModel.SaveChanges();
            return RedirectToAction("IndexNew");
        }
    }
}