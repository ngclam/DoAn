using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LapTrinhWebBanCaPhe.Context;

namespace LapTrinhWebBanCaPhe.Models
{
    public class HomeModel
    {
        public List<Product> ListProduct { get; set; }
        public List<Category> ListCategory { get; set; }
        public List<News> ListNews { get; set; }
        public List<Order> ListOrder { get; set; }
        public List<OrderDetail> ListOrderDetail { get; set; }


    }

}