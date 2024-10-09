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
    public class CatController : Controller
    {
        CAFESHOP_DBModel objCAFESHOP_DBModel = new CAFESHOP_DBModel();
        // GET: Admin/Product
        public ActionResult IndexCat()
        {
            var listCat = objCAFESHOP_DBModel.Categories.ToList();
            return View(listCat);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Category objCategory)
        {
            try
            {
                objCAFESHOP_DBModel.Categories.Add(objCategory);
                objCAFESHOP_DBModel.SaveChanges();
                return RedirectToAction("IndexCat");
            }
            catch (Exception)
            {
                return RedirectToAction("IndexCat");
            }
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            var objCategory = objCAFESHOP_DBModel.Categories.Where(n => n.Id == id).FirstOrDefault();
            return View(objCategory);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var objCategory = objCAFESHOP_DBModel.Categories.Where(n => n.Id == id).FirstOrDefault();

            objCAFESHOP_DBModel.Categories.Remove(objCategory);
            objCAFESHOP_DBModel.SaveChanges();
            return RedirectToAction("IndexCat");
        }
    }
}