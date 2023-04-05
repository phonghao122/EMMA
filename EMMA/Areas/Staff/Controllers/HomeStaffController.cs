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
            if (Session["user"] == null)
            {
                return Redirect("~/Login/Login");
            }
            else
            {
                var id = Session["id"].ToString();
                var nv = db.NHANVIEN.Find(id);
                return View(nv);
            }
        }

        public ActionResult CapNhatTTCN(string id)
        {
            if (Session["user"] == null)
            {
                return Redirect("~/Login/Login");
            }
            else
            {
                var nhanVien = db.NHANVIEN.Find(id);
                if (nhanVien.Avt != null)
                {
                    Session["Avt"] = nhanVien.Avt;
                }
                return View(nhanVien);
            }
        }

        [HttpPost]
        public ActionResult CapNhatTTCN(string id, NHANVIEN model, HttpPostedFileBase fileAnh)
        {
            if (fileAnh != null)
            {
                if (fileAnh.ContentLength > 0)
                {
                    string rootFolder = Server.MapPath("/Data/");
                    string pathImage = rootFolder + fileAnh.FileName;
                    fileAnh.SaveAs(pathImage);
                    model.Avt = "/Data/" + fileAnh.FileName;
                }
            }
            else
            {
                model.Avt = Session["Avt"].ToString();
            }
            var user = db.NHANVIEN.Find(model.Username);
            if (user == null)
            {
                var nhanVien = db.NHANVIEN.Find(model.MaNV);
                nhanVien.HoTenNV = model.HoTenNV;
                nhanVien.GioiTinh = model.GioiTinh;
                nhanVien.Username = model.Username;
                nhanVien.Password = model.Password;
                nhanVien.NgaySinh = model.NgaySinh;
                nhanVien.DiaChi = model.DiaChi;
                nhanVien.SDT = model.SDT;
                nhanVien.Email = model.Email;
                nhanVien.QueQuan = model.QueQuan;
                nhanVien.CCCD = model.CCCD;
                nhanVien.TrinhDoHocVan = model.TrinhDoHocVan;
                nhanVien.Avt = model.Avt;
                db.SaveChanges();
                return RedirectToAction("ThongTinCaNhan");
            }
            else
            {
                ModelState.AddModelError("", "Username Đã Tồn Tại");
                return View(model);
            }
            
        }

        public ActionResult ChamCong()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChamCong(ChamCong model, bool faceid)
        {
            model.Ngay = (int)DateTime.Now.Day;
            model.Thang = (int)DateTime.Now.Month;
            model.Nam = (int)DateTime.Now.Year;
            model.MaNV = Session["user_id"].ToString();
            if (faceid == true)
            {
                if(model.Vao == null)
                {
                    model.Vao = "Yes";
                    Session["Da Cham cong"] = "Vao";
                    db.ChamCong.Add(model);
                    db.SaveChanges();
                    return View();
                }
                else
                {
                    model.Ra = "Yes";
                    db.ChamCong.Add(model);
                    db.SaveChanges();
                    Session["Da Cham cong"] = "Ra";
                    return View();
                }
            }
            else
            {
                Session["Chua Cham Cong"] = "Yes";
                return View();
            }
        }
    }
}