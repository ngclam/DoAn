using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LapTrinhWebBanCaPhe.Context;

namespace LapTrinhWebBanCaPhe.Controllers
{
    public class MenuProductDetailController : Controller
    {
        CAFESHOP_DBModel objCAFESHOP_DBModel = new CAFESHOP_DBModel();
        public ActionResult Index(int Id)
        {
            var objProduct = objCAFESHOP_DBModel.Products.Where(n => n.ProId == Id).FirstOrDefault();
            return View(objProduct);
        }

    }
}