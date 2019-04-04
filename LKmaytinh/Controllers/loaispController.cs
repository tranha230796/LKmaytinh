using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LKmaytinh.Models;

namespace LKmaytinh.Controllers
{
    public class loaispController : Controller
    {
        //GET: loai
       dbMayTinhDataContext db = new dbMayTinhDataContext();
        public ActionResult loaisp()
        {
            return PartialView(db.Loais.Take(12).ToList());
        }
        //public ActionResult loaisp()
        //{
        //    var loaisp = from Loai in db.Loais select Loai;
        //    return PartialView(loaisp);
        //}
    }
}