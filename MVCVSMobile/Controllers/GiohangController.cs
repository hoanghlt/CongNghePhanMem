using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCVSMobile.Models;
namespace MVCVSMobile.Controllers
{
    public class GiohangController : Controller
    {
        //
        // GET: /Giohang/
        VSMobileDataContext data = new VSMobileDataContext();
        public List<Giohang> laygiohang()
        {
            List<Giohang> lstGiohang = Session["Giohang"] as List<Giohang>;
            if (lstGiohang == null)
            {
                lstGiohang = new List<Giohang>();
                Session["Giohang"] = lstGiohang;
            }
            return lstGiohang;
        }
        public ActionResult Giohangnull()
        {
            return View();
        }
        public ActionResult Themgiohang(int iMadt, string strURL)
        {
            List<Giohang> lstGiohang = laygiohang();
            Giohang sanpham = lstGiohang.Find(n => n.iMadt == iMadt); //kt sanpham co ton tai trong gio k ?
            if (sanpham == null)
            {
                sanpham = new Giohang(iMadt);
                lstGiohang.Add(sanpham);
                return Redirect(strURL);
            }
            else
            {
                sanpham.iSoluong++;
                return Redirect(strURL);
            }
}
        //xu li hien thi gio hang
        public ActionResult GioHang()
        {
            List<Giohang> lstGiohang = laygiohang();
            //if (lstGiohang.Count == 0)
            //{
            //    return RedirectToAction("Giohang");
            //}
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            return View(lstGiohang);
        }
        private int TongSoLuong()
        {
            int iTongSoLuong = 0;
            List<Giohang> lstGiohang = Session["Giohang"] as List<Giohang>;
            if (lstGiohang != null)
            {
                iTongSoLuong = lstGiohang.Sum(n => n.iSoluong);
            }
            return iTongSoLuong;
        }
        private double TongTien()
        {
            double iTongTien = 0;
            List<Giohang> lstGiohang = Session["Giohang"] as List<Giohang>;
            if (lstGiohang != null)
            {
                iTongTien = lstGiohang.Sum(n => n.dThanhtien);
            }
            return iTongTien;
        }       
        public ActionResult GiohangPartial()
        {
            List<Giohang> lstGiohang = laygiohang();
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Kiemtra = Session["Giohang"];
            if (Session["Giohang"] != null)
            {
                string tongtien = String.Format("{0:0,0}", TongTien());
                ViewBag.Tongtien = tongtien;

            }
            else
                ViewBag.Tongtien = 0;

            return PartialView(lstGiohang);
        }
        [HttpGet]
        public ActionResult Capnhatgiohang(int isp, FormCollection f)
        {
            List<Giohang> lstGiohang = laygiohang();
            Giohang sanpham = lstGiohang.SingleOrDefault(n => n.iMadt == isp);
            if (sanpham != null)
            {
                sanpham.iSoluong = int.Parse(f["txtSoluong"].ToString());
            }
            return RedirectToAction("GioHang");
        }
        public ActionResult Xoagiohang(int isp)
        {
            List<Giohang> lstGiohang = laygiohang();
            Giohang sanpham = lstGiohang.SingleOrDefault(n => n.iMadt == isp); // kt sanpham co trong gio k?
            if (sanpham != null)
            {
                lstGiohang.RemoveAll(n => n.iMadt == isp);
                return RedirectToAction("GioHang");
            }
            if (lstGiohang.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("GioHang");
        }
        public ActionResult Xoagiohangpartial(int isp)
        {
            List<Giohang> lstGiohang = laygiohang();
            Giohang sanpham = lstGiohang.SingleOrDefault(n => n.iMadt == isp); // kt sanpham co trong gio k?      
            lstGiohang.RemoveAll(n => n.iMadt == isp);
            return RedirectToAction("Index","Home");
        }   
        public ActionResult Xoatatcagio()
        {
            List<Giohang> lstGiohang = laygiohang();
            lstGiohang.Clear();
            return RedirectToAction("Giohang");
        }
        // hien thi viewdathang de cap nhat thong tin cho don hang
        [HttpGet]
        public ActionResult DatHang()
        {
            if(Session["Taikhoan"]==null || Session["Taikhoan"].ToString()=="")
            {
                return RedirectToAction("Dangnhap", "DangkyDangnhap");
            }
            
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            List<Giohang> lstGiohang = laygiohang();
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            return View(lstGiohang);
        }

        public ActionResult Xacnhandonhang()
        {
            return View();
        }
        public ActionResult DatHang(FormCollection collection)
        {
            //Them Don hang
            Order ddh = new Order();
            Customer kh = (Customer)Session["Taikhoan"];
            List<Giohang> gh = laygiohang();
            ddh.ID = kh.ID;
            ddh.CustomerName = kh.Name;
            ddh.CustomerMobile = kh.Phone;
            ddh.CustomerAddress = kh.Address;
            ddh.CustomerEmail = kh.Email;
            ddh.CreatedDate = DateTime.Now;
            //var ngaygiao = String.Format("{0:MM/dd/yyyy}", collection["Ngaygiao"]);
            //ddh.Ngaygiao = DateTime.Parse(ngaygiao);
            ddh.Status = false;
            //ddh.Dathanhtoan = false;
            data.Orders.InsertOnSubmit(ddh);
            data.SubmitChanges();
            //Them chi tiet don hang            
            foreach (var item in gh)
            {
                OrderDetail ctdh = new OrderDetail();
                ctdh.OrderID = ddh.ID;
                ctdh.ProductID = item.iMadt;
                ctdh.Quantity = item.iSoluong;
                ctdh.Price = (decimal)item.dDongia;
                data.OrderDetails.InsertOnSubmit(ctdh);
            }
            data.SubmitChanges();
            Session["GioHang"] = null;
            return RedirectToAction("Xacnhandonhang", "Giohang");
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}