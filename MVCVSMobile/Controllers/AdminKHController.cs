using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList.Mvc;
using PagedList;
using MVCVSMobile.Models;

namespace MVCVSMobile.Controllers
{
    public class AdminKHController : Controller
    {
        //
        // GET: /AdminKH/
        VSMobileDataContext db = new VSMobileDataContext();
        public ActionResult Index(int? page)
        {
                     
                int pageNumber = (page ?? 1);
                int pageSize = 10;
                return View(db.Customers.ToList().OrderBy(n => n.ID).ToPagedList(pageNumber, pageSize));
            
        }
        [HttpGet]
        public ActionResult Themkh()
        {
             User user = (User)Session["TaikhoanAdmin"];
            if (Session["TaikhoanAdmin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            if (user.GroupID == "ADMIN" || user.GroupID == "MOD")
            {
                return View();
            }
            return RedirectToAction("Loi404", "Pages");
        }
        [HttpPost]
        public ActionResult Themkh(Customer kh)
        {
            User user = (User)Session["TaikhoanAdmin"];
            if (Session["TaikhoanAdmin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            if (user.GroupID == "ADMIN" || user.GroupID == "MOD")
            {
                db.Customers.InsertOnSubmit(kh);
                db.SubmitChanges();
                var tenkh = kh.Name;
                TempData["ThongBao"] = "Đã thêm thành công khách hàng " + tenkh + " !";
                return RedirectToAction("Index");
            }
            return RedirectToAction("Loi404","Pages");
        }
        public ActionResult Details(int id)
        {
                var kh = (from a in db.Customers where a.ID == id select a);
                return View(kh.SingleOrDefault());
        }
        // xoa
        [HttpGet]
        public ActionResult Delete(int id)
        {
            User user = (User)Session["TaikhoanAdmin"];
            if (Session["TaikhoanAdmin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            if (user.GroupID == "ADMIN" || user.GroupID == "MOD")
            {
                Customer kh = db.Customers.SingleOrDefault(n => n.ID == id);
                ViewBag.ID = kh.ID;
                return View(kh);
            }
            return RedirectToAction("Loi404", "Pages");
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult Xacnhanxoa(int id)
        {
            Customer kh = db.Customers.SingleOrDefault(n => n.ID == id);
            ViewBag.ID = kh.ID;
            db.Customers.DeleteOnSubmit(kh);
            db.SubmitChanges();
            return RedirectToAction("Index");
        }
        //sua sp
        [HttpGet]
        public ActionResult Edit(int id)
        {
             User user = (User)Session["TaikhoanAdmin"];
            if (Session["TaikhoanAdmin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            if (user.GroupID == "ADMIN" || user.GroupID == "MOD")
            {
                var kh = (from a in db.Customers where a.ID == id select a);
                return View(kh.SingleOrDefault());
            }
            return RedirectToAction("Loi404","Pages");
        }
        [HttpPost]
        public ActionResult Edit(int id, Customer kh)
        {
            var ttkh = (from a in db.Customers where a.ID == id select a).SingleOrDefault();
            ttkh.Name = kh.Name;
            UpdateModel(ttkh);
            db.SubmitChanges();
            var tenkh = kh.Name;
            TempData["ThongBao"] = "Đã cập nhật lại thông tin khách hàng " + tenkh;
            return RedirectToAction("Index");
        }
	}
}