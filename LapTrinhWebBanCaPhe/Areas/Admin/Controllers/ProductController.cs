using LapTrinhWebBanCaPhe.Context;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LapTrinhWebBanCaPhe.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        CAFESHOP_DBModel objCAFESHOP_DBModel = new CAFESHOP_DBModel();
        // GET: Admin/Product
        public ActionResult Index()
        {
            var listProduct = objCAFESHOP_DBModel.Products.ToList();
            return View(listProduct);
        }


        [HttpGet]
        public ActionResult Create()
        {
            var categoriesList = objCAFESHOP_DBModel.Categories.ToList();
            ViewBag.CatId = new SelectList(categoriesList, "Id", "CatName");
            return View();
        }

        [HttpPost]
        public ActionResult Create(Product objProduct)
        {
            var categoriesList = objCAFESHOP_DBModel.Categories.ToList();
            ViewBag.CatId = new SelectList(categoriesList, "Id", "CatName");
            if (objProduct.ImageUpload != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(objProduct.ImageUpload.FileName);
                string extension = Path.GetExtension(objProduct.ImageUpload.FileName);
                fileName = fileName + "_" + long.Parse(DateTime.Now.ToString("yyyyMMddhhmmss")) + extension;
                objProduct.ProImage = fileName;
                objProduct.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/img/product/"), fileName));
            }
            objCAFESHOP_DBModel.Products.Add(objProduct);
            objCAFESHOP_DBModel.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Details(int id)
        {
            var objProduct = objCAFESHOP_DBModel.Products.Where(n => n.ProId == id).FirstOrDefault();
            return View(objProduct);
        }
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            var objProduct = objCAFESHOP_DBModel.Products.Where(n => n.ProId == id).FirstOrDefault();
            var categoriesList = objCAFESHOP_DBModel.Categories.ToList();
            ViewBag.CatId = new SelectList(categoriesList, "Id", "CatName");
            return View(objProduct);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var objProduct = objCAFESHOP_DBModel.Products.Where(n => n.ProId == id).FirstOrDefault();
            var categoriesList = objCAFESHOP_DBModel.Categories.ToList();
            ViewBag.CatId = new SelectList(categoriesList, "Id", "CatName");
            objCAFESHOP_DBModel.Products.Remove(objProduct);
            objCAFESHOP_DBModel.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var objProduct = objCAFESHOP_DBModel.Products.Where(n => n.ProId == id).FirstOrDefault();
            var categoriesList = objCAFESHOP_DBModel.Categories.ToList();
            ViewBag.CatId = new SelectList(categoriesList, "Id", "CatName");
            return View(objProduct);
        }
        [HttpPost]
        public ActionResult Edit(Product objProduct)
        {
            var categoriesList = objCAFESHOP_DBModel.Categories.ToList();
            ViewBag.CatId = new SelectList(categoriesList, "Id", "CatName");
            if (objProduct.ImageUpload != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(objProduct.ImageUpload.FileName);
                string extension = Path.GetExtension(objProduct.ImageUpload.FileName);
                fileName = fileName + "_" + long.Parse(DateTime.Now.ToString("yyyyMMddhhmmss")) + extension;
                objProduct.ProImage = fileName;
                objProduct.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/img/product"), fileName));
            }
            objCAFESHOP_DBModel.Entry(objProduct).State = EntityState.Modified;
            objCAFESHOP_DBModel.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}