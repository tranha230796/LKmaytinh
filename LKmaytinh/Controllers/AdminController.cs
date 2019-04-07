
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


        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SanPham()
        {
            if (Session["Taikhoanadmin"] == null || Session["Taikhoanadmin"].ToString() == "")
            {
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
        public ActionResult DonHang(int? page)
        {
            if (Session["Taikhoanadmin"] == null || Session["Taikhoanadmin"].ToString() == "")
            {
                return RedirectToAction("Login", "Admin");
            }
            int pageNumber = (page ?? 1);
            int pageSize = 7;
            return View(db.DonDatHangs.ToList().OrderBy(n => n.MaDH).ToPagedList(pageNumber, pageSize));
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(FormCollection collection)
        {
            var tendn = collection["username"];
            var matkhau = collection["password"];
            if (String.IsNullOrEmpty(tendn))
            {
                ViewData["Loi1"] = "Nhap ten username ";
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

        [HttpGet]
        public ActionResult ThemNSX()
        {
            ViewBag.MaNSX = new SelectList(db.NhaSXes.ToList().OrderBy(n => n.TenNSX), "MaNSX", "TenNSX");
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ThemNSX(NhaSX ns, FormCollection form)
        {
            NhaSX nsx = new NhaSX();
            nsx.TenNSX = form["TenNSX"];
            db.NhaSXes.InsertOnSubmit(nsx);
            db.SubmitChanges();
            return RedirectToAction("NSX");
        }

        [HttpGet]
        public ActionResult Themspmoi()
        {

            ViewBag.Maloai = new SelectList(db.Loais.ToList().OrderBy(n => n.Tenloai), "Maloai", "Tenloai");
            ViewBag.MaNSX = new SelectList(db.NhaSXes.ToList().OrderBy(n => n.TenNSX), "MaNSX", "TenNSX");
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Themspmoi(SanPham sanpham, HttpPostedFileBase fileUpload)
        {
            ViewBag.Maloai = new SelectList(db.Loais.ToList().OrderBy(n => n.Tenloai), "Maloai", "Tenloai");
            ViewBag.MaNSX = new SelectList(db.NhaSXes.ToList().OrderBy(n => n.TenNSX), "MaNSX", "TenNSX");
            if (fileUpload == null)
            {
                ViewBag.Thongbao = "Vui lòng chọn ảnh ";
                return View();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    var fileName = Path.GetFileName(fileUpload.FileName);
                    var path = Path.Combine(Server.MapPath("~/Hinhanh"), fileName);
                    if (System.IO.File.Exists(path))
                        ViewBag.Thongbao = "Hình ảnh đã tồn tại";
                    else
                    {
                        fileUpload.SaveAs(path);
                    }
                    sanpham.Anhbia = fileName;
                    db.SanPhams.InsertOnSubmit(sanpham);
                    db.SubmitChanges();
                }
                return RedirectToAction("SanPham");
            }
        }
        [HttpGet]
        public ActionResult SuaNSX(int id)
        {
            NhaSX nsx = db.NhaSXes.SingleOrDefault(n => n.MaNSX == id);
            ViewBag.MaNSX = nsx.MaNSX;
            if (nsx == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(nsx);
        }
        [HttpPost]
        [ValidateInput(false)]
        [ActionName("SuaNSX")]

        [HttpGet]
        public ActionResult SuaDonHang(int id)
        {
            //Lay ra doi tuong san pham theo ma
            DonDatHang donhang = db.DonDatHangs.SingleOrDefault(n => n.MaDH == id);
            ViewBag.MaDH = donhang.MaDH;
            if (donhang == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            return View(donhang);
        }
        [HttpPost]
        [ValidateInput(false)]
        [ActionName("SuaDonHang")]

        
        public ActionResult Chitiet(int id)
        {
            SanPham sanpham = db.SanPhams.SingleOrDefault(n => n.MaSP == id);
            ViewBag.MaSP = sanpham.MaSP;
            if (sanpham == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sanpham);
        }
        [HttpGet]
        public ActionResult Xoa(int id)
        {
            SanPham sanpham = db.SanPhams.SingleOrDefault(n => n.MaSP == id);
            ViewBag.MaSP = sanpham.MaSP;
            if (sanpham == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sanpham);

        }
        [HttpPost, ActionName("Xoa")]
        public ActionResult Xacnhanxoa(int id)
        {
            SanPham sanpham = db.SanPhams.SingleOrDefault(n => n.MaSP == id);
            ViewBag.MaSP = sanpham.MaSP;
            if (sanpham == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.SanPhams.DeleteOnSubmit(sanpham);
            db.SubmitChanges();
            return RedirectToAction("SanPham");
        }
        [HttpGet]
        public ActionResult Sua(int id)
        {
            SanPham sanpham = db.SanPhams.SingleOrDefault(n => n.MaSP == id);
            ViewBag.MaSP = sanpham.MaSP;
            if (sanpham == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.Loai = new SelectList(db.Loais.ToList().OrderBy(n => n.Tenloai), "Maloai", "Tenloai", sanpham.Maloai);
            ViewBag.MaNSX = new SelectList(db.NhaSXes.ToList().OrderBy(n => n.TenNSX), "MaNSX", "TenNSX", sanpham.MaNSX);
            return View(sanpham);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Sua(SanPham sanpham, HttpPostedFileBase fileUpload)
        {
            ViewBag.Loai = new SelectList(db.Loais.ToList().OrderBy(n => n.Tenloai), "Maloai", "Tenloai", sanpham.Maloai);
            ViewBag.MaNSX = new SelectList(db.NhaSXes.ToList().OrderBy(n => n.TenNSX), "MaNSX", "TenNSX", sanpham.MaNSX);
            SanPham sp = db.SanPhams.SingleOrDefault(n => n.MaSP == sanpham.MaSP);
            if (fileUpload == null)
            {
                ViewBag.Thongbao = "Vui lòng chọn ảnh bìa";
                return View(sp);
            }
            else
            {
                var fileName = Path.GetFileName(fileUpload.FileName);
                var path = Path.Combine(Server.MapPath("~/Hinhanh"), fileName);

                if (System.IO.File.Exists(path))
                {
                    sp.TenSP = sanpham.TenSP;
                    sp.Ngaycapnhat = sanpham.Ngaycapnhat;
                    sp.Anhbia = fileName;
                    sp.Gia = sanpham.Gia;
                    sp.Mota = sanpham.Mota;
                    sp.Ngaycapnhat = DateTime.Now;
                    sp.Maloai = sanpham.Maloai;
                    sp.MaNSX = sanpham.MaNSX;
                }
                else
                {

                    sp.TenSP = sanpham.TenSP;
                    sp.Ngaycapnhat = sanpham.Ngaycapnhat;
                    sp.Anhbia = fileName;
                    sp.Gia = sanpham.Gia;
                    sp.Mota = sanpham.Mota;
                    sp.Ngaycapnhat = DateTime.Now;
                    sp.Maloai = sanpham.Maloai;
                    sp.MaNSX = sanpham.MaNSX;

                }
                db.SubmitChanges();
                sanpham.Anhbia = fileName;

                return RedirectToAction("SanPham");
            }
        }
    }
}