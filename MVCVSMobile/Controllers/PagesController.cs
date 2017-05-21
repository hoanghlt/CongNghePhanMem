using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCVSMobile.Models;
using PagedList.Mvc;
using PagedList;
using System.Configuration;
namespace MVCVSMobile.Controllers
{
    public class PagesController : Controller
    {
        //
        VSMobileDataContext data = new VSMobileDataContext();
        // GET: /Pages/
        public ActionResult Lienhe()
        {
            return View();
        }
        public ActionResult Gioithieu()
        {
            return View();
        }
        public ActionResult Hethongcuahang()
        {
            return View();
        }
        public ActionResult Chitiettintuc(int id)
        {
            var sp = from s in data.Posts
                     where s.ID == id
                     select s;       
            return View(sp.Single());
        }
        public ActionResult Tintuc()
        {
            LoadRSS rss = new LoadRSS("http://www.24h.com.vn/upload/rss/congnghethongtin.rss");
            ViewBag.title = rss.title;
            ViewBag.description = rss.description;
            ViewBag.language = rss.language;
            ViewBag.link = rss.link;
            ViewBag.data = rss.items;
            ViewBag.Menu = rss.items;
            //----------------------load trang 24 gio

            return View();
        }
        public ActionResult Menutintuc()
        {
           
            return PartialView();
        }
        public ActionResult Loi404()
        {
             return View();
        }
	}
}