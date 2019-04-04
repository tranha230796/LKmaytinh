
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
                return RedirectToAction("Login", "Admin");
            }
            return View(db.SanPhams.ToList());
        }
    }
}

