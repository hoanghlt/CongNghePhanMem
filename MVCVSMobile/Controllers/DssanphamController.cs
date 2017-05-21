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
    public class DssanphamController : Controller
    {
        //
        VSMobileDataContext data = new VSMobileDataContext();
        // GET: /Dssanpham/    

        public ActionResult Sptheomenu(int id, int? page)
        {
         
            int pageSize = 6;
            int pageNumber = (page ?? 1);
            var spmenu = from sp in data.Products where sp.CategoryID == id select sp;
            //ViewBag.ListSize = new Product().ListSize(id);
            return View(spmenu.OrderBy(n => n.Name).ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Menuleft()
        {
            var menu = from m in data.ProductCategories where m.ParentID == 1 select m;
            return View(menu.ToList());

        }
        [ChildActionOnly]
        public PartialViewResult ChildrenMenu(int ParentID)
        {
            var _menu = from menu in data.ProductCategories where menu.ParentID == ParentID select menu;
            return PartialView(_menu.ToList());
        }
        private List<Product> Laysp()
        {
            return data.Products.ToList();
        }
        public ActionResult Allsp(int? page)
        {
            int pageSize = 6;
            int pageNumber = (page ?? 1);
            var sp = Laysp();
            return View(sp.ToPagedList(pageNumber, pageSize));
        }
        // GET: /Dssanpham/  
        [HttpPost]
        public ActionResult KQTimkiem(FormCollection collection)
        {
            string sTukhoa = collection["Timkiem"].ToString();
            ViewBag.Tukhoa = sTukhoa;

                List<Product> lstKQTK = data.Products.Where(n => n.Name.Contains(sTukhoa)).ToList();

                if (lstKQTK.Count == 0)
                {
                    ViewBag.Thongbao = "Không tìm thấy sản phẩm ";
                    return RedirectToAction("Index", "Home");
                }
                ViewBag.Thongbao = "Đã tìm thấy " + lstKQTK.Count + "sản phẩm";
                return View();
            
        }        


    }
}