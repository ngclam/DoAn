using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LapTrinhWebBanCaPhe.Context;
using LapTrinhWebBanCaPhe.Models;
using System.Data.Entity;

namespace LapTrinhWebBanCaPhe.Controllers
{
    public class MenuController : Controller
    {
        CAFESHOP_DBModel objCAFESHOP_DBModel = new CAFESHOP_DBModel();

        public ActionResult Index()
        {
            {
                //Sử dụng nhiều bảng dữ liệu trong 1 trang
                HomeModel objHomeModel = new HomeModel();
                objHomeModel.ListCategory = objCAFESHOP_DBModel.Categories.ToList();
                objHomeModel.ListProduct = objCAFESHOP_DBModel.Products.ToList();
                return View(objHomeModel);
            }
        }
        public ActionResult ProductCategory(int Id)
        {
            HomeModel objHomeModel = new HomeModel();
            objHomeModel.ListCategory = objCAFESHOP_DBModel.Categories.ToList();
            objHomeModel.ListProduct = objCAFESHOP_DBModel.Products.Where(n=> n.CatId == Id).ToList();
            return View(objHomeModel);
        }
    }
}