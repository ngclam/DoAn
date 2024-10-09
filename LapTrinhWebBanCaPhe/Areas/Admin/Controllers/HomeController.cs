using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LapTrinhWebBanCaPhe.Context;
using LapTrinhWebBanCaPhe.Models;
using System.Data.SqlClient;

namespace LapTrinhWebBanCaPhe.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        CAFESHOP_DBModel db = new CAFESHOP_DBModel();
        // GET: Admin/Home
        public ActionResult Index()
        {
            ViewBag.countNew = db.News.Count();
            ViewBag.countProduct = db.Products.Count();
            ViewBag.countOrder = db.Orders.Count();
            ViewBag.countPrice = db.OrderDetails.Sum(x=> x.Price * x.Quantity);
            return View();
        }
    }
}