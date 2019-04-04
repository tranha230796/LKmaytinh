using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LKmaytinh.Models;

using PagedList;
using PagedList.Mvc;

namespace LKmaytinh.Controllers
{
    public class MayTinhController : Controller
    {
        // GET: MayTinh
        dbMayTinhDataContext maytinhdb = new dbMayTinhDataContext();
        public ActionResult Index(int ? page)
        {
            int pageSize = 6;
            int pageNum = (page ?? 1);
            var mayt = from maytinh in maytinhdb.SanPhams select maytinh;
            return View(mayt.ToPagedList(pageNum,pageSize));
        }

        [HttpGet]
        public ActionResult TimKiem (List<SanPham> maytinhs, int? page)
        {
            int pageNum = (page ?? 1);
            int pageSize = 6;
            return View(maytinhs.ToPagedList(pageNum, pageSize));
        }

        [HttpPost]
        public ActionResult TimKiem(FormCollection form)
        {
            int pageSize = 6;
            var maytinhs = maytinhdb.SanPhams.Where(n => n.TenSP.Contains(form["tensp"]));
            return TimKiem(maytinhs.ToList(),null);
        }

        public ActionResult SPtheoloai(int id)
        {
            var sp = from s in maytinhdb.SanPhams where s.Maloai == id select s;
            return View(sp);
        }
        public  ActionResult Details(int id)
        {
            var sp = from s in maytinhdb.SanPhams where s.MaSP == id select s;
            return View(sp.Single());
        }
        [HttpGet]
        public ActionResult DangKy()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangKy(FormCollection collection, KhachHang kh)
        {
            var hoten = collection["HotenKH"];
            var tendn = collection["TenDN"];
            var matkhau = collection["MatKhau"];
            var nhaplaimatkhau = collection["Nhaplaimatkhau"];
            var diachi = collection["Diachi"];
            var email = collection["Email"];
            var dienthoai = collection["Dienthoai"];
            var ngaysinh = String.Format("{0:MM/dd/yyyy}", collection["Ngaysinh"]);
            if (String.IsNullOrEmpty(hoten))
            {
                ViewData["Loi1"] = "Họ tên khách hàng không được để trống";
            }
            else if (String.IsNullOrEmpty(tendn))
            {
                ViewData["Loi2"] = "Nhập tên đăng nhập";
            }
            else if (String.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi3"] = "Nhập mật khẩu";
            }
            else if (String.IsNullOrEmpty(nhaplaimatkhau))
            {
                ViewData["Loi4"] = "Nhập lại mật khẩu";
            }
            if (String.IsNullOrEmpty(email))
            {
                ViewData["Loi5"] = "email không được đẻ trống";
            }
            if (String.IsNullOrEmpty(dienthoai))
            {
                ViewData["Loi6"] = "Phải nhập điện thoại";
            }
            else
            {
                kh.HoTen = hoten;
                kh.Taikhoan = tendn;
                kh.Matkhau = matkhau;
                kh.email = email;
                kh.DiachiKH = diachi;
                kh.DienthoaiKH = dienthoai;
                kh.Ngaysinh = DateTime.Parse(ngaysinh);
                maytinhdb.KhachHangs.InsertOnSubmit(kh);
                maytinhdb.SubmitChanges();
                return RedirectToAction("Index");
            }
            return this.DangKy();
        }
        [HttpGet]
        public ActionResult Dangnhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Dangnhap(FormCollection collection)
        {
            var tendn = collection["TenDN"];
            var matkhau = collection["Matkhau"];
            if(String.IsNullOrEmpty(tendn))
            {
                ViewData["Loi1"] = "Phải nhập tên đăng nhập";
            }
            else if (String.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi2"] = "Phải nhập mật khẩu";
            }
            else
            {
                KhachHang kh = maytinhdb.KhachHangs.SingleOrDefault(n => n.Taikhoan == tendn && n.Matkhau == matkhau);
                if (kh != null)
                {
                    ViewBag.Thongbao = "Chúc mừng đăng nhập thành công";
                    Session["Taikhoan"] = kh;
                }
                else
                {
                    ViewBag.Thongbao = "Tên tài khoản hoặc mật khẩu không đúng";
                    return View();
                }
                
            }
            return this.Dangnhap();
        }
        public ActionResult LienHe()
        {
            return View();
        }
        
    }
}







