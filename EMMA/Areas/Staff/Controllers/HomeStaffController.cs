using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EMMA.Areas.Staff.Controllers
{
    public class HomeStaffController : Controller
    {
        // GET: Staff/HomeStaff
        public ActionResult Index()
        {
            return View();
        }
    }
}