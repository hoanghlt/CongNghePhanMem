using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList.Mvc;
using PagedList;
using MVCVSMobile.Models;
using System.IO;

namespace MVCVSMobile.Controllers
{
    public class AdminTintucController : Controller
    {
        //
        // GET: /AdminTintuc/
        VSMobileDataContext db = new VSMobileDataContext();
        public ActionResult Index(int? page)
        {
            var session = Session["TaikhoanAdmin"];
            if (Session["TaikhoanAdmin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            int pageNumber = (page ?? 1);
            int pageSize = 5;
            return View(db.News.ToList().OrderBy(n => n.ID).ToPagedList(pageNumber, pageSize));

        }
        [HttpGet]
        public ActionResult Them()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Them(New tt, HttpPostedFileBase fileupload)
        {
            //kt duong dan file
            if (fileupload == null)
            {
                ViewBag.Thongbao = "Chọn ảnh bìa";
                return View();
            }
            //them vao csdl
            else
            {
                if (ModelState.IsValid)
                {
                    //luu ten file
                    var fileName = Path.GetFileName(fileupload.FileName);
                    // luu duong dan
                    var path = Path.Combine(Server.MapPath("~/Demo/Hinhanh"), fileName);
                    //kt hinh anh da co?
                    if (System.IO.File.Exists(path))
                    {
                        ViewBag.Thongbao = "Hình ảnh đã tồn tại!";
                    }
                    else
                    {
                        //luu hinh anh vao duong dan
                        fileupload.SaveAs(path);
                    }
                    tt.Image = fileName;
                    //luu vao csdl
                    db.News.InsertOnSubmit(tt);
                    db.SubmitChanges();
                }
                return RedirectToAction("Index");
            }
        }
        public ActionResult Details(int id)
        {
            var tt = (from a in db.News where a.ID == id select a);
            return View(tt.SingleOrDefault());
        }
        // xoa
        [HttpGet]
        public ActionResult Delete(int id)
        {
            New tt = db.News.SingleOrDefault(n => n.ID == id);
            ViewBag.ID = tt.ID;
            return View(tt);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult Xacnhanxoa(int id)
        {
            New tt = db.News.SingleOrDefault(n => n.ID == id);
            ViewBag.ID = tt.ID;
            db.News.DeleteOnSubmit(tt);
            db.SubmitChanges();
            return RedirectToAction("Index");
        }
        //sua sp
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var tt = (from a in db.News where a.ID == id select a);
            return View(tt.SingleOrDefault());
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(New tt, HttpPostedFileBase fileupload)
        {
            if (fileupload == null)
            {
                ViewBag.Thongbao = "Vui lòng chọn ảnh bìa!";
                return View();
            }
            //them vao csdl
            else
            {
                if (ModelState.IsValid)
                {
                    var fileName = Path.GetFileName(fileupload.FileName);
                    var path = Path.Combine(Server.MapPath("~/Demo/Hinhanh"), fileName);
                    if (System.IO.File.Exists(path))
                    {
                        ViewBag.Thongbao = "Hình ảnh đã tồn tại!";
                    }
                    else
                    {
                        fileupload.SaveAs(path);
                    }
                    tt.Image = fileName;
                    UpdateModel(tt);
                    db.SubmitChanges();
                }
                return RedirectToAction("Index");
            }
        }
    }
}