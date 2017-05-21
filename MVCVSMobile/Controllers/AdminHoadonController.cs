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
    public class AdminHoadonController : Controller
    {
        //
        // GET: /AdminHoadon/
        VSMobileDataContext db = new VSMobileDataContext();
        public ActionResult Index(int? page)
        {
            if (Session["TaikhoanAdmin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            int pageNumber = (page ?? 1);
            int pageSize = 10;
            return View(db.Orders.ToList().OrderBy(n => n.ID).ToPagedList(pageNumber, pageSize));

        }
        [HttpGet]
        public ActionResult Them()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Them(Order kh)
        {
            db.Orders.InsertOnSubmit(kh);
            db.SubmitChanges();
            var tenkh = kh.ID;
            TempData["ThongBao"] = "Đã thêm thành công hóa đơn khách hàng " + tenkh + " !";
            return RedirectToAction("Index");
        }
        public ActionResult Details(int id)
        {
            var kh = (from a in db.Orders where a.ID == id select a);
            return View(kh.SingleOrDefault());
        }
        // xoa
        [HttpGet]
        public ActionResult Delete(int id)
        {
            Order kh = db.Orders.SingleOrDefault(n => n.ID == id);
            ViewBag.ID = kh.ID;
            return View(kh);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult Xacnhanxoa(int id)
        {
            Order kh = db.Orders.SingleOrDefault(n => n.ID == id);
            ViewBag.ID = kh.ID;
            db.Orders.DeleteOnSubmit(kh);
            db.SubmitChanges();
            return RedirectToAction("Index");
        }
        //sua sp
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var kh = (from a in db.Orders where a.ID == id select a);
            return View(kh.SingleOrDefault());
        }
        [HttpPost]
        public ActionResult Edit(int id, Order kh)
        {
            var ttkh = (from a in db.Orders where a.ID == id select a).SingleOrDefault();
            ttkh.CustomerName = kh.CustomerName;
            UpdateModel(ttkh);
            db.SubmitChanges();
            var tenkh = kh.CustomerName;
            TempData["ThongBao"] = "Đã cập nhật lại thông tin hóa đơn khách hàng " + tenkh;
            return RedirectToAction("Index");
        }
	}
}