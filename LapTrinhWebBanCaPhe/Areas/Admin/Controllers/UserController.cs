using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using LapTrinhWebBanCaPhe.Context;

namespace LapTrinhWebBanCaPhe.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        CAFESHOP_DBModel objCAFESHOP_DBModel = new CAFESHOP_DBModel();
        public ActionResult Index()
        {
            var listUser = objCAFESHOP_DBModel.Users.OrderBy(x => x.isAdmin == false).ToList();
            return View(listUser);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User objUser)
        {
            objUser.UserPassword = GetMD5(objUser.UserPassword);
            objCAFESHOP_DBModel.Users.Add(objUser);
            objCAFESHOP_DBModel.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Details(int id)
        {
            var objUser = objCAFESHOP_DBModel.Users.Where(n => n.UserId == id).FirstOrDefault();
            return View(objUser);
        }
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            var objUser = objCAFESHOP_DBModel.Users.Where(n => n.UserId == id).FirstOrDefault();
            return View(objUser);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var objUser = objCAFESHOP_DBModel.Users.Where(n => n.UserId == id).FirstOrDefault();

            objCAFESHOP_DBModel.Users.Remove(objUser);
            objCAFESHOP_DBModel.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var objUser = objCAFESHOP_DBModel.Users.Where(n => n.UserId == id).FirstOrDefault();
            return View(objUser);
        }
        [HttpPost]
        public ActionResult Edit(User objUser)
        {
            objUser.UserPassword = GetMD5(objUser.UserPassword);
            objCAFESHOP_DBModel.Entry(objUser).State = EntityState.Modified;
            objCAFESHOP_DBModel.SaveChanges();
            return RedirectToAction("Index");
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