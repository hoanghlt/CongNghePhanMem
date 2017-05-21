using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCVSMobile.Models;
using PagedList.Mvc;
using PagedList;
using System.IO;

namespace MVCVSMobile.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/
        VSMobileDataContext db = new VSMobileDataContext();
     
        public ActionResult Index()
        {
          
            if(Session["TaikhoanAdmin"]==null)
            {
                return RedirectToAction("Login", "Admin");
            }       
            return View();
        }

        public ActionResult Product(int? page)
        {
            User user = (User)Session["TaikhoanAdmin"];
            if (Session["TaikhoanAdmin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            if (user.GroupID == "ADMIN" || user.GroupID=="MOD")
            {
                int pageNumber = (page ?? 1);
                int pageSize = 10;
                return View(db.Products.ToList().OrderBy(n => n.ID).ToPagedList(pageNumber, pageSize));
                //return View(db.Products.ToList());
               
            }            
            return RedirectToAction("Loi404", "Pages");
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection collection)
        {
            var tendn = collection["userName"];
            var matkhau = collection["password"];
            if(String.IsNullOrEmpty(tendn))
            {
                ViewData["Loi1"] = "Nhập tên đăng nhập";
            }
            else if(String.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi2"] = "Phải nhập mật khẩu";
            }
            else
            {
                User ad = db.Users.SingleOrDefault(n => n.UserName == tendn && n.Password == matkhau);
                if (ad != null)
                {
                    Session["TaikhoanAdmin"] = ad;
                    return RedirectToAction("Index", "Admin");
                }
                else
                    ViewBag.Thongbao = "Tên đăng nhập hoặc mật khẩu không đúng";
            }
            return View();
        }
        [HttpGet]
        public ActionResult Themsanpham()
        {
            //lay ten nha san xuat tu id, sap xep theo ten.
            ViewBag.CategoryID = new SelectList(db.ProductCategories.ToList().OrderBy(n => n.Name), "ID", "Name");
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Themsanpham(Product sp, HttpPostedFileBase fileupload)
        {
            //dua du lieu vao dropdownload
            ViewBag.CategoryID = new SelectList(db.ProductCategories.ToList().OrderBy(n => n.Name), "ID", "Name");
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
                    var path = Path.Combine(Server.MapPath("~/Demo/Hinhanh/Products"), fileName);
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
                    sp.Image = fileName;
                    //luu vao csdl
                    db.Products.InsertOnSubmit(sp);
                    db.SubmitChanges();
                }
                return RedirectToAction("Product");
            }
        }
        public ActionResult Details(int id)
        {

            Product sp = db.Products.SingleOrDefault(n => n.ID == id);
            ViewBag.ID = sp.ID;
            if(sp==null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sp);
        }
        // xoa
        [HttpGet]
        public ActionResult Delete(int id)
        {
            Product sp = db.Products.SingleOrDefault(n => n.ID == id);
            ViewBag.ID = sp.ID;
            if(sp==null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sp);
        }
        [HttpPost,ActionName("Delete")]
        public ActionResult Xacnhanxoa(int id)
        {
            Product sp = db.Products.SingleOrDefault(n => n.ID == id);
            ViewBag.ID = sp.ID;
            if(sp==null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.Products.DeleteOnSubmit(sp);
            db.SubmitChanges();
            return RedirectToAction("Product");
        }
        //sua sp
        [HttpGet]
        public ActionResult Edit(int id)
        {
            Product sp = db.Products.SingleOrDefault(n => n.ID == id);
            if(sp==null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.CategoryID = new SelectList(db.ProductCategories.ToList().OrderBy(n => n.Name), "ID", "Name");
            return View(sp);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Product sp, HttpPostedFileBase fileupload)
        {
            ViewBag.CategoryID = new SelectList(db.ProductCategories.ToList().OrderBy(n => n.Name), "ID", "Name");
            if(fileupload==null)
            {
                ViewBag.Thongbao = "Vui lòng chọn ảnh bìa!";
                return View();
            }
                //them vao csdl
            else
            {
                if(ModelState.IsValid)
                {
                    var fileName = Path.GetFileName(fileupload.FileName);
                    var path = Path.Combine(Server.MapPath("~/Demo/Hinhanh/Products"), fileName);
                    if(System.IO.File.Exists(path))
                    {
                        ViewBag.Thongbao = "Hình ảnh đã tồn tại!";
                    }
                    else
                    {
                        fileupload.SaveAs(path);
                    }
                    sp.Image = fileName;
                    UpdateModel(sp);
                    db.SubmitChanges();
                }
                return RedirectToAction("Product");
            }
        }
        
	}
}