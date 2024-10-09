using LapTrinhWebBanCaPhe.Context;
using LapTrinhWebBanCaPhe.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LapTrinhWebBanCaPhe.Areas.Admin.Controllers
{
    public class OrderController : Controller
    {
        CAFESHOP_DBModel objCAFESHOP_DBModel = new CAFESHOP_DBModel();
        // GET: Admin/Order
        public ActionResult IndexOrder()
        {
            HomeModel objHomeModel = new HomeModel();
            objHomeModel.ListOrder = objCAFESHOP_DBModel.Orders.OrderByDescending(x => x.OrderDate).ToList();
            objHomeModel.ListOrderDetail = objCAFESHOP_DBModel.OrderDetails.ToList();
            return View(objHomeModel);
        }

        public ActionResult OrderDetails(int Id)
        {
            var listOrderDetail = objCAFESHOP_DBModel.OrderDetails.Where(n => n.OrderId == Id).ToList();
            return View(listOrderDetail);
        }
    }
}