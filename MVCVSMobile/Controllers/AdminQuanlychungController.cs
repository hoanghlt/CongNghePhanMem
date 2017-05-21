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
    public class AdminQuanlychungController : Controller
    {
        //
        // GET: /AdminQuanlychung/
        VSMobileDataContext db = new VSMobileDataContext();
        public ActionResult SlideIndex(int? page)
        {
            if (Session["TaikhoanAdmin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            int pageNumber = (page ?? 1);
            int pageSize = 10;
            return View(db.Slides.ToList().OrderBy(n => n.ID).ToPagedList(pageNumber, pageSize));

        }
        [HttpGet]
        public ActionResult Themslide()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Themslide(Slide kh)
        {
            db.Slides.InsertOnSubmit(kh);
            db.SubmitChanges();
            var tenkh = kh.ID;
            TempData["ThongBao"] = "Đã thêm thành công slide " + tenkh + " !";
            return RedirectToAction("SlideIndex");
        }
        public ActionResult Details(int id)
        {
            var kh = (from a in db.Slides where a.ID == id select a);
            return View(kh.SingleOrDefault());
        }
        // xoa
        [HttpGet]
        public ActionResult Delete(int id)
        {
            Slide kh = db.Slides.SingleOrDefault(n => n.ID == id);
            ViewBag.ID = kh.ID;
            return View(kh);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult Xacnhanxoa(int id)
        {
            Slide kh = db.Slides.SingleOrDefault(n => n.ID == id);
            ViewBag.ID = kh.ID;
            db.Slides.DeleteOnSubmit(kh);
            db.SubmitChanges();
            return RedirectToAction("SlideIndex");
        }
        //sua sp
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var kh = (from a in db.Slides where a.ID == id select a);
            return View(kh.SingleOrDefault());
        }
        [HttpPost]
        public ActionResult Edit(int id, Slide kh)
        {
            var ttkh = (from a in db.Slides where a.ID == id select a).SingleOrDefault();
            ttkh.Name = kh.Name;
            UpdateModel(ttkh);
            db.SubmitChanges();
            var tenkh = kh.Name;
            TempData["ThongBao"] = "Đã cập nhật lại thông tin slide " + tenkh;
            return RedirectToAction("SlideIndex");
        }

         //Phan hoi
        public ActionResult PhanhoiIndex(int? page)
        {
            if (Session["TaikhoanAdmin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            int pageNumber = (page ?? 1);
            int pageSize = 10;
            return View(db.Feedbacks.ToList().OrderBy(n => n.ID).ToPagedList(pageNumber, pageSize));

        }
        [HttpGet]
        public ActionResult Themph()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Themph(Feedback kh)
        {
            db.Feedbacks.InsertOnSubmit(kh);
            db.SubmitChanges();
            var tenkh = kh.Name;
            TempData["ThongBao"] = "Đã thêm thành công phản hồi " + tenkh + " !";
            return RedirectToAction("PhanhoiIndex");
        }
        public ActionResult Detailph(int id)
        {
            var kh = (from a in db.Feedbacks where a.ID == id select a);
            return View(kh.SingleOrDefault());
        }
        // xoa
        [HttpGet]
        public ActionResult Deleteph(int id)
        {
            Feedback kh = db.Feedbacks.SingleOrDefault(n => n.ID == id);
            ViewBag.ID = kh.ID;
            return View(kh);
        }
        [HttpPost, ActionName("Deleteph")]
        public ActionResult Xacnhanph(int id)
        {
            Feedback kh = db.Feedbacks.SingleOrDefault(n => n.ID == id);
            ViewBag.ID = kh.ID;
            db.Feedbacks.DeleteOnSubmit(kh);
            db.SubmitChanges();
            return RedirectToAction("PhanhoiIndex");
        }
        //sua sp
        [HttpGet]
        public ActionResult Editph(int id)
        {
            var kh = (from a in db.Feedbacks where a.ID == id select a);
            return View(kh.SingleOrDefault());
        }
        [HttpPost]
        public ActionResult Editph(int id, Feedback kh)
        {
            var ttkh = (from a in db.Feedbacks where a.ID == id select a).SingleOrDefault();
            ttkh.Name = kh.Name;
            UpdateModel(ttkh);
            db.SubmitChanges();
            var tenkh = kh.Name;
            TempData["ThongBao"] = "Đã cập nhật lại thông tin phản hồi " + tenkh;
            return RedirectToAction("PhanhoiIndex");
        }

	}
}