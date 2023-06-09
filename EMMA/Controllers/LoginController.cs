﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using EMMA.Models;

namespace EMMA.Controllers
{
    public class LoginController : Controller
    {
        EMMAEntities db = new EMMAEntities();
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            var nhanVien = db.NHANVIEN.SingleOrDefault(m => m.Username.ToLower() == username.ToLower() && m.Password == password);
            if (nhanVien != null)
            {
                if(nhanVien.Role == 1)
                {
                    Session["user"] = nhanVien;
                    Session["id"] = nhanVien.MaNV;
                    return Redirect("~/Manager/HomeManager/Index");
                }
                else
                {
                    Session["user"] = nhanVien;
                    Session["id"] = nhanVien.MaNV;
                    return Redirect("~/Staff/HomeStaff/Index");
                }
            }
            else
            {
                TempData["error"] = "Sai Username hoặc Password. Vui lòng nhập lại!";
                return View();
            }
        }

        public ActionResult Logout() 
        {
            Session.Remove("user");
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
        
    }
}