
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

 		public ActionResult Index()
        {
            return View();
        }
        // GET: Admin
		public ActionResult SanPham()
        {
            if (Session["Taikhoanadmin"] == null || Session["Taikhoanadmin"].ToString() == "")
            {
				Console.Write("Chưa đăng nhập.");
                return RedirectToAction("Login", "Admin");
            }
            return View(db.SanPhams.ToList());
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

    }
}

