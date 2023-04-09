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
            if (Session["user"] == null)
            {
                return Redirect("~/Login/Login");
            }
            else
            {
                List<NHANVIEN> dsNV = db.NHANVIEN.ToList();
                return View(dsNV);
            }
        }

        public ActionResult ThemMoiNV()
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

        [HttpPost]
        public ActionResult ThemMoiNV(NHANVIEN model, HttpPostedFileBase fileAnh)
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
            if(model.MaNV == null)
            {
                ModelState.AddModelError("", "Chưa Nhập Mã Nhân Viên");
                return View(model);
            }
            else
            {
                var nv = db.NHANVIEN.Find(model.MaNV);
                if (nv == null)
                {
                    var user = db.NHANVIEN.Find(model.Username);
                    if(user == null)
                    {
                        db.NHANVIEN.Add(model);
                        db.SaveChanges();
                        return RedirectToAction("DanhSachNV");
                    }   
                    else
                    {
                        ModelState.AddModelError("", "Username Đã Tồn Tại");
                        return View(model);
                    }    
                }
                else
                {
                    ModelState.AddModelError("", "Mã Nhân Viên Đã Tồn Tại");
                    return View(model);
                }    
            }    
            
        }

        public ActionResult CapNhatNV(string id)
        {
            if (Session["user"] == null)
            {
                return Redirect("~/Login/Login");
            }
            else
            {
                var nhanVien = db.NHANVIEN.Find(id);
                if(nhanVien.Avt != null)
                {
                    Session["Avt"] = nhanVien.Avt;
                }
                return View(nhanVien);
            }
        }

        [HttpPost]
        public ActionResult CapNhatNV(string id, NHANVIEN model, HttpPostedFileBase fileAnh)
        {
            if(fileAnh != null)
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
                nhanVien.MaPB = model.MaPB;
                nhanVien.MaCV = model.MaCV;
                nhanVien.BacLuong = model.BacLuong;
                nhanVien.Avt = model.Avt;
                db.SaveChanges();
                return RedirectToAction("DanhSachNV");
            }
            else
            {
                ModelState.AddModelError("", "Username Đã Tồn Tại");
                return View(model);
            }
            
        }

        public ActionResult XoaNV(string id)
        {
            if (Session["user"] == null)
            {
                return Redirect("~/Login/Login");
            }
            else
            {
                var nhanVien = db.NHANVIEN.FirstOrDefault(m => m.MaNV == id);
                db.NHANVIEN.Remove(nhanVien);
                db.SaveChanges();
                return RedirectToAction("DanhSachNV");
            }
        }

        public ActionResult ChiTietNV(string id)
        {
            if (Session["user"] == null)
            {
                return Redirect("~/Login/Login");
            }
            else
            {
                var nv = db.NHANVIEN.Find(id);
                return View(nv);
            }
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
        public ActionResult DanhSachPB()
        {
            if (Session["user"] == null)
            {
                return Redirect("~/Login/Login");
            }
            else
            {
                List<PHONGBAN> pb = db.PHONGBAN.ToList();
                return View(pb);
            }
        }

        public ActionResult ThemPB()
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

        [HttpPost]
        public ActionResult ThemPB(PHONGBAN model)
        {
            db.PHONGBAN.Add(model);
            db.SaveChanges();
            return RedirectToAction("DanhSachPB");
        }

        public ActionResult EditPB(string id)
        {
            if (Session["user"] == null)
            {
                return Redirect("~/Login/Login");
            }
            else
            {
                var pb = db.PHONGBAN.Find(id);
                return View(pb);
            }
        }

        [HttpPost]
        public ActionResult EditPB(PHONGBAN model, string id)
        {
            var pb = db.PHONGBAN.Find(model.MaPB);
            pb.TenPB = model.TenPB;
            db.SaveChanges();
            return RedirectToAction("DanhSachPB");
        }

        public ActionResult DeletePB(string id)
        {
            var pb = db.PHONGBAN.Find(id);
            db.PHONGBAN.Remove(pb);
            db.SaveChanges();
            return RedirectToAction("DanhSachPB");
        }
    }
}