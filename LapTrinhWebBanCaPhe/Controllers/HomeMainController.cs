using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using LapTrinhWebBanCaPhe.Context;
using LapTrinhWebBanCaPhe.Models;

namespace LapTrinhWebBanCaPhe.Controllers
{
    public class HomeMainController : Controller
    {
        //tạo obj
        CAFESHOP_DBModel objCAFESHOP_DBModel = new CAFESHOP_DBModel();
        public ActionResult Index()
        {
            //Sử dụng nhiều bảng dữ liệu trong 1 trang
            HomeModel objHomeModel = new HomeModel();
            objHomeModel.ListCategory = objCAFESHOP_DBModel.Categories.ToList();
            objHomeModel.ListProduct = objCAFESHOP_DBModel.Products.ToList();
            objHomeModel.ListNews = objCAFESHOP_DBModel.News.ToList();
            return View(objHomeModel);
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User user)
        {
            if(ModelState.IsValid)
            {
                if(objCAFESHOP_DBModel.Users.Any(n=>n.Email == user.Email))
                {
                    ViewBag.errormssg = "Email đã tồn tại,nhập email khác!";
                    return View(user);
                }    
            }
            user.UserPassword = GetMD5(user.UserPassword);
            objCAFESHOP_DBModel.Users.Add(user);
            objCAFESHOP_DBModel.SaveChanges();
            return RedirectToAction("Login");
        }

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User user)
        {
            var email = user.Email;
            var f_password = GetMD5(user.UserPassword);
            var userCheck = objCAFESHOP_DBModel.Users.FirstOrDefault(x => x.Email.Equals(email) && x.UserPassword.Equals(f_password));
            if (userCheck != null && userCheck.isAdmin == true)
            {
                Session["User"] = userCheck;       
                return RedirectToAction("Index", "Home", new { area = "Admin" });
            }
            else if(userCheck != null && userCheck.isAdmin != true)
            {
                Session["User"] = userCheck;
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.LoginFail = "Tài khoản hoặc mật khẩu sai";
                return View("Login");
            }
        }
        public ActionResult Logout()
        {
            Session["User"] = null;
            return RedirectToAction("Index","HomeMain");
        }

        private string GetMD5(string userPassword)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(userPassword);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");
            }
            return byte2String;
        }
    }
}