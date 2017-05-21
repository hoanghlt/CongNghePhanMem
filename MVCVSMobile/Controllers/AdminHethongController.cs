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
    public class AdminHethongController : Controller
    {
        //
        // GET: /AdminHethong/
        VSMobileDataContext db = new VSMobileDataContext();
        public ActionResult Index(int? page)
        {
            User user = (User)Session["TaikhoanAdmin"];
            if (Session["TaikhoanAdmin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            if (user.GroupID == "ADMIN")
            {              
                int pageNumber = (page ?? 1);
                int pageSize = 10;
                return View(db.Users.ToList().OrderBy(n => n.ID).ToPagedList(pageNumber, pageSize));
            }
            return RedirectToAction("Loi404", "Pages");
          
        }
        [HttpGet]
        public ActionResult Themuser()
        {
            User user = (User)Session["TaikhoanAdmin"];
            if (Session["TaikhoanAdmin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            if (user.GroupID == "ADMIN")
            {
                ViewBag.GroupID = new SelectList(db.UserGroups.ToList().OrderBy(n => n.Name), "ID", "Name");
                return View();
            }
            return RedirectToAction("Loi404", "Pages"); 
        }
        [HttpPost]
        public ActionResult Themuser(User u)
        {
            ViewBag.GroupID = new SelectList(db.UserGroups.ToList().OrderBy(n => n.Name), "ID", "Name");
            db.Users.InsertOnSubmit(u);
            db.SubmitChanges();
            var tennd = u.Name;
            TempData["ThongBao"] = "Đã thêm thành công khách hàng " + tennd + " !";
            return RedirectToAction("Index");
        }
        public ActionResult DetailsUser(int id)
        {
            User user = (User)Session["TaikhoanAdmin"];
            if (Session["TaikhoanAdmin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            if (user.GroupID == "ADMIN")
            {
                User u = db.Users.SingleOrDefault(n => n.ID == id);
                ViewBag.ID = u.ID;
                if (u == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                return View(u);
            }
            return RedirectToAction("Loi404", "Pages");
        }
        // xoa
        [HttpGet]
        public ActionResult DeleteUser(int id)
        {
               User user = (User)Session["TaikhoanAdmin"];
            if (Session["TaikhoanAdmin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            if (user.GroupID == "ADMIN")
            {
                User u = db.Users.SingleOrDefault(n => n.ID == id);
                ViewBag.ID = u.ID;
                if (u == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                return View(u);
            }
            return RedirectToAction("Loi404", "Pages");
        }
        [HttpPost, ActionName("DeleteUser")]
        public ActionResult Xacnhanxoa(int id)
        {
            User u = db.Users.SingleOrDefault(n => n.ID == id);
            ViewBag.ID = u.ID;
            if (u == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.Users.DeleteOnSubmit(u);
            db.SubmitChanges();
            return RedirectToAction("Index");
        }
        //sua sp
        [HttpGet]
        public ActionResult EditUser(int id)
        {
               User user = (User)Session["TaikhoanAdmin"];
            if (Session["TaikhoanAdmin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            if (user.GroupID == "ADMIN")
            {
                User u = db.Users.SingleOrDefault(n => n.ID == id);
                if (u == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                ViewBag.GroupID = new SelectList(db.UserGroups.ToList().OrderBy(n => n.Name), "ID", "Name");
                return View(u);
            }
            return RedirectToAction("Loi404", "Pages");
        }
        [HttpPost]
        public ActionResult EditUser(User u)
        {
            ViewBag.GroupID = new SelectList(db.UserGroups.ToList().OrderBy(n => n.Name), "ID", "Name");           
            UpdateModel(u);
            db.SubmitChanges();
            return RedirectToAction("Index");            
        }
        // controller user group
        public ActionResult UserGroup(int? page)
        {
             User user = (User)Session["TaikhoanAdmin"];
            if (Session["TaikhoanAdmin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            if (user.GroupID == "ADMIN")
            {
                int pageNumber = (page ?? 1);
                int pageSize = 10;
                return View(db.UserGroups.ToList().OrderBy(n => n.ID).ToPagedList(pageNumber, pageSize));
            }
            return RedirectToAction("Loi404", "Pages");
        }
        [HttpGet]
        public ActionResult Themnhom()
        {
               User user = (User)Session["TaikhoanAdmin"];
            if (Session["TaikhoanAdmin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            if (user.GroupID == "ADMIN")
            {
                //ViewBag.ID = new SelectList(db.UserGroups.ToList().OrderBy(n => n.ID), "ID","ID");
                return View();
            }
            return RedirectToAction("Loi404", "Pages");
        }
        [HttpPost]
        public ActionResult Themnhom(UserGroup nh)
        {
            //ViewBag.ID = new SelectList(db.UserGroups.ToList().OrderBy(n => n.ID), "ID","ID");
            db.UserGroups.InsertOnSubmit(nh);
            db.SubmitChanges();
            var tennh = nh.Name;
            return RedirectToAction("UserGroup");
        }
        public ActionResult Chitietnhom(string id)
        {
               User user = (User)Session["TaikhoanAdmin"];
            if (Session["TaikhoanAdmin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            if (user.GroupID == "ADMIN")
            {
                var nh = (from a in db.UserGroups where a.ID == id select a);
                return View(nh.SingleOrDefault());
            }
            return RedirectToAction("Loi404","Pages");
        }
        // xoa
        [HttpGet]
        public ActionResult Xoanhom(string id)
        {
            User user = (User)Session["TaikhoanAdmin"];
            if (Session["TaikhoanAdmin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            if (user.GroupID == "ADMIN")
            {
                UserGroup nh = db.UserGroups.SingleOrDefault(n => n.ID == id);
                ViewBag.ID = nh.ID;
                return View(nh);
            }
            return RedirectToAction("Loi404","Pages");
        }
        [HttpPost, ActionName("Xoanhom")]
        public ActionResult Xacnhanxoanhom(string id)
        {

            UserGroup nh= db.UserGroups.SingleOrDefault(n => n.ID == id);
            ViewBag.ID = nh.ID;
            db.UserGroups.DeleteOnSubmit(nh);
            db.SubmitChanges();
            return RedirectToAction("UserGroup");
        }
        //sua sp
        [HttpGet]
        public ActionResult Suanhom(string id)
        {
               User user = (User)Session["TaikhoanAdmin"];
            if (Session["TaikhoanAdmin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            if (user.GroupID == "ADMIN")
            {
                var nh = (from a in db.UserGroups where a.ID == id select a);
                return View(nh.SingleOrDefault());
            }
            return RedirectToAction("Loi404", "Pages");
        }
        [HttpPost]
        public ActionResult Suanhom(string id, UserGroup nh)
        {
            var ttnh = (from a in db.UserGroups where a.ID == id select a).SingleOrDefault();
            ttnh.Name = nh.Name;
            UpdateModel(ttnh);
            db.SubmitChanges();
            var tenkh = nh.Name;
            return RedirectToAction("UserGroup");
        }
	}
}