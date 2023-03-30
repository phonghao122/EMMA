using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EMMA.Models;

namespace EMMA.Areas.Staff.Controllers
{
    public class HomeStaffController : Controller
    {
        EMMAEntities db = new EMMAEntities();
        // GET: Staff/HomeStaff
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ThongTinCaNhan()
        {
            var id = Session["id"].ToString();
            var nv = db.NHANVIEN.Find(id);
            return View(nv);
        }
    }
}