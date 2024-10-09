using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LapTrinhWebBanCaPhe.Context;
using LapTrinhWebBanCaPhe.Models;

namespace LapTrinhWebBanCaPhe.Controllers
{
    public class CartController : Controller
    {
        CAFESHOP_DBModel objCAFESHOP_DBModel = new CAFESHOP_DBModel();
        // GET: Cart
        public ActionResult Index()
        {
            return View((List<CartModel>)Session["cart"]);
        }
        public ActionResult AddToCart(int id, int quantity)
        {
            if (Session["cart"] == null && (int)(quantity) > 0)
            {
                List<CartModel> cart = new List<CartModel>();
                cart.Add(new CartModel { Product = objCAFESHOP_DBModel.Products.Find(id), Quantity = quantity });
                Session["cart"] = cart;
                Session["count"] = 1;
            }
            else if ((int)(quantity) > 0)
            {
                List<CartModel> cart = (List<CartModel>)Session["cart"];
                //kiểm tra sản phẩm có tồn tại trong giỏ hàng chưa???
                int index = isExist(id);
                if (index != -1)
                {
                    //nếu sản phẩm tồn tại trong giỏ hàng thì cộng thêm số lượng
                    cart[index].Quantity += quantity;
                }
                else
                {
                    //nếu không tồn tại thì thêm sản phẩm vào giỏ hàng
                    cart.Add(new CartModel { Product = objCAFESHOP_DBModel.Products.Find(id), Quantity = quantity });
                    //Tính lại số sản phẩm trong giỏ hàng
                    Session["count"] = Convert.ToInt32(Session["count"]) + 1;
                }
                Session["cart"] = cart;
            }
            return Json(new { Message = "Thành công", JsonRequestBehavior.AllowGet });
        }
        private int isExist(int? id)
        {
            List<CartModel> cart = (List<CartModel>)Session["cart"];
            for (int i = 0; i < cart.Count; i++)
                if (cart[i].Product.ProId == id)
                    return i;
            return -1;
        }
        //xóa sản phẩm khỏi giỏ hàng theo id
        public ActionResult Remove(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int check = isExist(id);
            List<CartModel> ListCart = (List<CartModel>)Session["cart"];
            ListCart.RemoveAt(check);
            if (ListCart.Count == 0)
            {
                Session["cart"] = null;
            }
            else
            {
                Session["cart"] = ListCart;
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult UpdateCart(FormCollection field, int quantity)
        {
            if ((int)(quantity) > 0)
            {
                string[] quantities = field.GetValues("quantity");
                List<CartModel> cart = (List<CartModel>)Session["cart"];
                for (int i = 0; i < cart.Count; i++)
                    cart[i].Quantity = Convert.ToInt32(quantities[i]);
                Session["cart"] = cart;
                return View("Index");
            }
            else
            {
                Session["cart"] = null;
                return RedirectToAction("Index");
            }    
        }
        public ActionResult ClearCart()
        {
            Session["cart"] = null;
            return RedirectToAction("Index");
        }
    }

}