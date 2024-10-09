using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LapTrinhWebBanCaPhe.Context;
using System.Data.Entity;
using PagedList;

namespace LapTrinhWebBanCaPhe.Controllers
{
    public class SearchController : Controller
    {
        // GET: Search
        CAFESHOP_DBModel db = new CAFESHOP_DBModel();
        public ActionResult SearchResult(string sTuKhoa, int? page)
        {
            //tìm kiếm theo tên sản phẩm
            var lstProduct = db.Products.Where(n => n.ProName.Contains(sTuKhoa));
            return View(lstProduct.OrderBy(n => n.ProName));
        }
    }
}