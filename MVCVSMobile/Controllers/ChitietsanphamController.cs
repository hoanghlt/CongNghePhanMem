using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCVSMobile.Models;
namespace MVCVSMobile.Controllers
{
    public class ChitietsanphamController : Controller
    {
        //
        VSMobileDataContext data = new VSMobileDataContext();
        // GET: /Chitietsanpham/
        public ActionResult Chitiet(int id)

        {
            var sp = from s in data.Products
                     where s.ID == id
                     select s;
            ViewBag.breadcrumbs = sp.Select(s=>s.Name);
            return View(sp.Single());
        }
	}
}