using LapTrinhWebBanCaPhe.Context;
using LapTrinhWebBanCaPhe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LapTrinhWebBanCaPhe.Controllers
{
    public class CheckoutController : Controller
    {
        CAFESHOP_DBModel objCAFESHOP_DBModel = new CAFESHOP_DBModel();
        private string strCart = "Cart";
        // GET: Checkout
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ProcessOrder(FormCollection field)
        {
            List<CartModel> ListCart = (List<CartModel>)Session[strCart];

            //1.Lưu đơn hàng vào bảng Order
            var order = new LapTrinhWebBanCaPhe.Context.Order()
            {
                CustomerFirstName = field["cusFirstName"],
                CustomerLastName = field["cusLastName"],
                CustomerAddress = field["cusAddress"],
                CustomerPhone = field["cusPhone"],
                CustomerEmail = field["cusEmail"],
                CustomerNote = field["cusNote"],
                OrderDate = DateTime.Now,
                PaymentType = field["cusPaymentType"],  
            };
            objCAFESHOP_DBModel.Orders.Add(order);
            objCAFESHOP_DBModel.SaveChanges();

            //2.Lưu thông tin đơn hàng vào bảng Order Detail
            foreach(CartModel cart in ListCart)
            {
                OrderDetail orderDetail = new OrderDetail()
                {
                    OrderId = order.OrderId,
                    ProductId = cart.Product.ProId,
                    Quantity = Convert.ToInt32(cart.Quantity),
                    Price = Convert.ToDouble(cart.Product.ProPrice)
                };
                objCAFESHOP_DBModel.OrderDetails.Add(orderDetail);
                objCAFESHOP_DBModel.SaveChanges();
            }

            //3.Xóa đơn hàng
            Session.Remove(strCart);

            return View("OrderSuccess");
        }
        public ActionResult OrderSuccess()
        {
            return View();
        }
    }
}