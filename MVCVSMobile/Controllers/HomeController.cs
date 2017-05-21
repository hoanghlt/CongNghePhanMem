using MVCVSMobile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace MVCVSMobile.Controllers
{
    public class HomeController : Controller
    {
        VSMobileDataContext data = new VSMobileDataContext();
        public ActionResult Index()
        {
            return View();
        }
 
        //  BansachDataContext data = new BansachDataContext();
        private List<Product> Layspmoi(int count)
        {
            //Sắp xếp sách theo ngày cập nhật, sau đó lấy top @count 
            return data.Products.OrderByDescending(a =>a.UpdatedDate).Take(count).ToList();
            //    return data.SACHes.OrderByDescending(a => a.Ngaycapnhat).Take(count).ToList();
        }
        private List<Product> Layspbanchay1(int count)
        {
            //Sắp xếp sách theo ngày cập nhật, sau đó lấy top @count 
            return data.Products.OrderByDescending(a => a.QuantitySold).Take(count).ToList();
            //    return data.SACHes.OrderByDescending(a => a.Ngaycapnhat).Take(count).ToList();
        }
        private List<Product> Layspbanchay2(int count)
        {
            return data.Products.OrderByDescending(a => a.QuantitySold).Skip(5).Take(count).ToList();
        }
        private List<Product> Layspnoibat(int count)
        {
            return data.Products.OrderByDescending(a => a.ViewCount).Take(count).ToList();
        }
        private List<Product> Bosuutapios(int count)
        {
            return data.Products.OrderByDescending(a => a.Name).Where(b => b.OSystem == "IOS").Take(count).ToList();
        }
        private List<Product> Bosuutapandroid(int count)
        {
            return data.Products.OrderByDescending(a => a.Name).Where(b=>b.OSystem=="Andoid").Take(count).ToList();
        }
        private List<Product> Layspgioithieu(int count)
        {
            //Sắp xếp sách theo ngày cập nhật, sau đó lấy top @count 
            return data.Products.OrderByDescending(a=>a.Size).Take(count).ToList();
            //    return data.SACHes.OrderByDescending(a => a.Ngaycapnhat).Take(count).ToList();
        }
        public ActionResult Spmoi()
        {
            var spmoi = Layspmoi(4);
            return PartialView(spmoi);
        }
        public ActionResult Spbanchay1()
        {
            var spbanchay = Layspbanchay1(5);
            return PartialView(spbanchay);
        }     
        public ActionResult Spbanchay2()
        {
            var spbanchay = Layspbanchay2(5);
            return PartialView(spbanchay);
        }
        public ActionResult Spnoibat()
        {
            var spnoibat = Layspnoibat(4);
            return PartialView(spnoibat);
        }
        public ActionResult BosuutapIOS()
        {
            var bosuutap = Bosuutapios(5);
            return PartialView(bosuutap);
        }
        public ActionResult BosuutapAndroid()
        {
            var bosuutap = Bosuutapandroid(5);
            return PartialView(bosuutap);
        }
        public ActionResult Spgioithieu()
        {
            var spnoibat = Layspgioithieu(6);
            return PartialView(spnoibat);
        }
        public ActionResult Menutop()
        {
            var menu = from m in data.ProductCategories where m.ParentID==1 select m;
            return View(menu.ToList());

        }
        [ChildActionOnly]
        public PartialViewResult ChildrenMenu(int ParentID)
        {
            var _menu=from menu in data.ProductCategories where menu.ParentID==ParentID select menu;
            return PartialView(_menu.ToList());
        }
    }
}