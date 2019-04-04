
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LKmaytinh.Models;
using System.IO;
using PagedList;
using PagedList.Mvc;


namespace LKmaytinh.Controllers
{
    public class AdminController : Controller
    {
        dbMayTinhDataContext db = new dbMayTinhDataContext();

 		[HttpPost]
        public ActionResult Login(FormCollection collection)
        {
            var tendn = collection["username"];
            var matkhau = collection["password"];
            if (String.IsNullOrEmpty(tendn))
            {
				Console.Write("Chưa đăng nhập.");
                return RedirectToAction("Login", "Admin");
            }
            else if (String.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi2"] = "Nhap password";
            }
            else
            {
                Admin ad = db.Admins.SingleOrDefault(n => n.UserAdmin == tendn && n.PassAdmin == matkhau);
                if (ad != null)
                {
                    Session["Taikhoanadmin"] = ad;
                    return RedirectToAction("Index", "Admin");
                }
                else
                    ViewBag.Thongbao = "Ten dang nhap sai or mat khau khong dung ";
            }
            return this.Login();
        }
		public ActionResult NSX()
        {
            return View(db.NhaSXes.ToList());
            //var sp = from s in db.SanPhams where s.Maloai == id select s;
            //return View(sp);
        }
		[HttpGet]
        public ActionResult Login()
        {
            return View();
        }

		[HttpGet]
        public ActionResult Logina()
        {
            return View();
        }
    }
}

