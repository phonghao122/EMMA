using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EMMA.Models;

namespace EMMA.Areas.Manager.Controllers
{
    public class HomeManagerController : Controller
    {
        EMMAEntities db = new EMMAEntities();

        public ActionResult Index()
        {
            if (Session["user"] == null)
            {
                return Redirect("~/Login/Login");
            }
            else
            {
                return View();
            }
        }

        public ActionResult DanhSachNV()
        {
            List<NHANVIEN> dsNV = db.NHANVIEN.ToList();
            return View(dsNV);
        }

        public ActionResult ThemMoiNV()
        {

            return View();
        }

        [HttpPost]
        public ActionResult ThemMoiNV(NHANVIEN model)
        {
            db.NHANVIEN.Add(model);
            db.SaveChanges();
            return RedirectToAction("DanhSachNV");
        }

        public ActionResult CapNhatNV(string id)
        {
            var nhanVien = db.NHANVIEN.Find(id);
            return View(nhanVien);
        }

        [HttpPost]
        public ActionResult CapNhatNV(string id, NHANVIEN model)
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
            nhanVien.MaPB = model.MaPB;
            nhanVien.MaCV = model.MaCV;
            nhanVien.BacLuong = model.BacLuong;
            nhanVien.Avt = model.Avt;
            db.SaveChanges();
            return RedirectToAction("DanhSachNV");
        }

        public ActionResult XoaNV(string id)
        {
            var nhanVien = db.NHANVIEN.FirstOrDefault(m => m.MaNV == id);
            db.NHANVIEN.Remove(nhanVien);
            db.SaveChanges();
            return RedirectToAction("DanhSachNV");
        }

        public ActionResult ThongTinCaNhan()
        {
            return View();
        }
    }
}