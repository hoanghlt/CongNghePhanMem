using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCVSMobile.Models
{
    public class Giohang
    {
        VSMobileDataContext data = new VSMobileDataContext();
        public int iMadt { set; get; }
        public string sTendt { set; get; }
        public string sAnhbia { set; get; }
        public Double dDongia { set; get; }
        public int iSoluong { set; get; }
        public double dThanhtien
        {
            get { return iSoluong * dDongia; }
        }
        public Giohang(int Madt)
        {
            iMadt = Madt;
            Product sp = data.Products.Single(n => n.ID == iMadt);
            sTendt = sp.Name;
            sAnhbia = sp.Image;
            dDongia = double.Parse(sp.Price.ToString());
            iSoluong = 1;
        }
    }
}