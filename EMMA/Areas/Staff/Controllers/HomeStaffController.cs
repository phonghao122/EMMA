using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using EMMA.Models;
using PagedList;

namespace EMMA.Areas.Staff.Controllers
{
    public class HomeStaffController : Controller
    {
        EMMAEntities db = new EMMAEntities();
        // GET: Staff/HomeStaff
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

        //Thông Tin Cá Nhân
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


        //Công
        public ActionResult DsCong(int? currentNam, int? currentThang, int? nam, int? thang, int? page)
        {
            if (Session["user"] == null)
            {
                return Redirect("~/Login/Login");
            }
            else
            {
                var dsLuong = new List<ChamCong>();
                if (thang != null && nam != null)
                {
                    page = 1;
                }
                else
                {
                    thang = currentThang;
                    nam = currentNam;
                }
                string id = Session["id"].ToString();
                if (string.IsNullOrEmpty(thang.ToString()) || string.IsNullOrEmpty(nam.ToString()))
                {
                    foreach(var cong in db.ChamCong.ToList())
                    {
                        if(cong.MaNV == id && cong.Thang == DateTime.Now.Month && cong.Nam == DateTime.Now.Year)
                        {
                            dsLuong.Add(cong);
                        }    
                    }    
                }
                else
                {
                    foreach (var cong in db.ChamCong.ToList())
                    {
                        if (cong.MaNV == id && cong.Thang == thang && cong.Nam == nam)
                        {
                            dsLuong.Add(cong);
                        }
                    }
                }
                ViewBag.currentThang = thang;
                ViewBag.currentNam = nam;
                int pageSize = 10;
                int pageNumber = (page ?? 1);
                return View(dsLuong.ToPagedList(pageNumber, pageSize));
            }
        }



        //Tong Hop Cong
        public ActionResult TongHopCong()
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
        public ActionResult TongHopCong(int thang, int nam)
        {
            CONG model = new CONG();
            foreach (var a in db.CONG.ToList())
            {
                if (a.Thang == thang && a.Nam == nam)
                {
                    db.CONG.Remove(a);
                }
            }
            var nv = db.ChamCong.FirstOrDefault(m => m.Thang == thang && m.Nam == nam);
            if (nv != null)
            {
                string maNV = Session["id"].ToString();
                List<ChamCong> dsCong = new List<ChamCong>();
                foreach (var item in db.ChamCong.ToList())
                {
                    if (item.MaNV == maNV && item.Thang == thang && item.Nam == nam && item.Vao != null && item.Ra != null)
                    {
                        dsCong.Add(item);
                    }
                }
                model.MaNV = maNV;
                model.SoNgayCong = dsCong.Count;
                model.SoNgayNghi = DateTime.DaysInMonth(nam, thang) - model.SoNgayCong;
                model.Thang = thang;
                model.Nam = nam;
                db.CONG.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }
    }
}