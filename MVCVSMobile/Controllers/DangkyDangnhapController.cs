using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCVSMobile.Models;
using MVCVSMobile.Controllers;
namespace MVCVSMobile.Controllers
{
    public class DangkyDangnhapController : Controller
    {
        //
        // GET: /Dangky/
        VSMobileDataContext db = new VSMobileDataContext();
        public ActionResult index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Dangky()
        {
            return View();
        }
        public ActionResult Dangxuat()
        {
            Session["Taikhoan"] = null;
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
         public ActionResult Doimatkhau()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Doimatkhau(Customer kh, FormCollection collection)
        {
            var matkhaucu =Mahoa.MD5Hash(collection["Matkhaucu"]);
            var matkhaumoi =Mahoa.MD5Hash(collection["Matkhaumoi"]);
            
            if (string.IsNullOrEmpty(matkhaucu))
            {
                ViewData["Loi1"] = "Phải nhập mật khẩu cũ";
            }
            else if (string.IsNullOrEmpty(matkhaumoi))
            {
                ViewData["Loi2"] = "Phải nhập mật khẩu mới";
            }
            Customer tk = (Customer)Session["Taikhoan"];
            if(tk.Password!=matkhaucu)
            {
                ViewBag.Thongbao = "Mật khẩu cũ không đúng";
            }
            else
            {
                var khachhang = db.Customers.SingleOrDefault(x => x.ID == tk.ID);
                khachhang.Password = matkhaumoi;
                UpdateModel(khachhang);
                db.SubmitChanges();
                return RedirectToAction("Index","Home");
            }
            return this.Doimatkhau();
        }
        public ActionResult Taikhoan(FormCollection collection)
        {
            var tendn = collection["Tendn"];
            var matkhau = collection["Matkhau"];
            if (string.IsNullOrEmpty(tendn))
            {
                ViewData["Loi1"] = "Phải nhập tài khoản";
            }
            else if (string.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi2"] = "Phải nhập mật khẩu";
            }
            else
            {
                Customer kh = db.Customers.FirstOrDefault(n => n.UserName == tendn && n.Password == matkhau);
                Customer kiemtra = db.Customers.FirstOrDefault(n => n.UserName == tendn);
                if (kh != null)
                {
                    ViewBag.Thongbao = "Đăng nhập thành công!";
                    Session["Taikhoan"] = kh;
                    return RedirectToAction("index", "Home");
                }
                else
                {
                    if (kiemtra != null)
                    {
                        ViewBag.Thongbao = "Mật khẩu không đúng";
                    }
                    else
                    {
                        ViewBag.Thongbao = "Tên đăng nhập không tồn tại";
                    }
                }

            }
            return View();
        }
        [HttpPost]
        public ActionResult Dangky(FormCollection collection, Customer kh)
        {
            var hoten = collection["HotenKH"];
            var tendn = collection["TenDN"];
            var mk = collection["Matkhau"];
            var matkhau = Mahoa.MD5Hash(mk);
            var matkhaunhaplai = collection["Matkhaunhaplai"];
            var diachiemail = collection["Email"];
            var diachi = collection["Diachi"];
            var dienthoai = collection["Dienthoai"];
            var ngaysinh = String.Format("{0: MM/dd/yyyy}", collection["Ngaysinh"]);
            Customer kiemtra = db.Customers.FirstOrDefault(n => n.UserName == tendn);

            if (String.IsNullOrEmpty(hoten))
            {
                ViewData["Loi1"] = "Họ tên không được để trống";
            }
            if (string.IsNullOrEmpty(tendn))
            {
                ViewData["Loi2"] = "Tên đăng nhập không được trống";
            }
            if (string.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi3"] = "Phải nhập mật khẩu";
            }
            if (string.IsNullOrEmpty(matkhaunhaplai))
            {
                ViewData["Loi4"] = "Phải nhập mật khẩu";
            }
            if (string.IsNullOrEmpty(dienthoai))
            {
                ViewData["Loi5"] = "Chưa nhập điện thoại";
            }
            if (string.IsNullOrEmpty(diachiemail))
            {
                ViewData["Loi6"] = "Chưa nhập địa chỉ email";
            }
            if (string.IsNullOrEmpty(diachi))
            {
                ViewData["Loi7"] = "Chưa nhập địa chỉ khách hàng";
            }

            else
            {
                if (kiemtra != null)
                {
                    ViewData["Loi2"] = "Tên đăng nhập đã tồn tại";
                }
                else
                {
                    if (mk != matkhaunhaplai)
                    {
                        ViewData["Loi4"] = "Mật khẩu nhập lại không đúng";
                    }
                    else
                    {

                        kh.Name = hoten;
                        kh.UserName = tendn;
                        kh.Password = matkhau;
                        kh.Email = diachiemail;
                        kh.Address = diachi;
                        kh.Phone = dienthoai;
                        kh.CreatedDate = DateTime.Now;
                        kh.Status = true;
                        db.Customers.InsertOnSubmit(kh);
                        db.SubmitChanges();
                        return RedirectToAction("DangNhap");
                    }
                }
            }
            return this.Dangky();
        }
        [HttpGet]
        public ActionResult Dangnhap()
        {
            return View();
        }
        public ActionResult Dangnhap(FormCollection collection)
        {
            var tendn = collection["Tendn"];
            var matkhau =Mahoa.MD5Hash(collection["Matkhau"]);
            if (string.IsNullOrEmpty(tendn))
            {
                ViewData["Loi1"] = "Phải nhập tài khoản";
            }
            else if (string.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi2"] = "Phải nhập mật khẩu";
            }
            else
            {
                Customer kh = db.Customers.SingleOrDefault(n => n.UserName == tendn && n.Password == matkhau);
                Customer kiemtra = db.Customers.SingleOrDefault(n => n.UserName == tendn);
                if (kiemtra == null)
                {
                    ViewBag.Thongbao = "Tên đăng nhập không tồn tại";
                }
                else
                {
                    if (kiemtra.Status == true)
                    {
                        if (kh == null)
                        {
                            ViewBag.Thongbao = "Mật khẩu không đúng";
                        }
                        else
                        {
                            ViewBag.Thongbao = "Đăng nhập thành công!";
                            Session["Taikhoan"] = kh;
                            return RedirectToAction("Index", "Home");
                        }
                       
                    }
                    else
                    {
                        ViewBag.Thongbao = "Tài khoản bị khóa";
                    }
                }
              
            }
            return View();
        }
    }
}