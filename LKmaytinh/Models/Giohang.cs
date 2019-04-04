using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LKmaytinh.Models;


namespace LKmaytinh.Models
{
    public class Giohang
    {
        dbMayTinhDataContext data = new dbMayTinhDataContext();
        public int iMaSP { set; get; }
        public String sTenSP { set; get; }
        public String sAnhbia { set; get; }
        public Double dDongia { set; get; }
        public int iSoluong { set; get; }
        public Double dThanhtien
        {
            get { return iSoluong * dDongia; }
        }
        public Giohang(int MaSP)
        {
            iMaSP = MaSP;
            SanPham sp = data.SanPhams.Single(n => n.MaSP == iMaSP);
            sTenSP = sp.TenSP;
            sAnhbia = sp.Anhbia;
            dDongia = double.Parse(sp.Gia.ToString());
            iSoluong = 1;
        }
    }
}