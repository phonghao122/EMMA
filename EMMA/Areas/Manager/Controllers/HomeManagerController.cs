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

        //Nhân Viên
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
                    if(model.Username == null || model.Password == null)
                    {
                        ModelState.AddModelError("", "Bắt buộc nhập Username và Password");
                        return View(model);
                    }    
                    else
                    {
                        var user = db.NHANVIEN.FirstOrDefault(m => m.Username == model.Username);
                        if (user == null)
                        {
                            if (model.MaCV == null || model.MaPB == null)
                            {
                                ModelState.AddModelError("", "Bắt buộc nhập Chức Vụ và Phòng Ban");
                                return View(model);
                            }
                            else
                            {
                                if (model.MaCV == "GD" || model.MaCV == "QL")
                                {
                                    model.Role = 1;
                                }
                                else
                                {
                                    model.Role = 2;
                                }
                                db.NHANVIEN.Add(model);
                                db.SaveChanges();
                                return RedirectToAction("DanhSachNV");
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("", "Username Đã Tồn Tại");
                            return View(model);
                        }
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
                if(nhanVien.MaCV != null)
                {
                    Session["cv"] = nhanVien.MaCV;
                }
                if (nhanVien.MaPB != null)
                {
                    Session["pb"] = nhanVien.MaPB;
                }
                if (nhanVien.BacLuong != null)
                {
                    Session["bl"] = nhanVien.BacLuong;
                }
                if (nhanVien.TrinhDoHocVan != null)
                {
                    Session["hv"] = nhanVien.TrinhDoHocVan;
                }
                if (nhanVien.GioiTinh != null)
                {
                    Session["gt"] = nhanVien.GioiTinh;
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
            if(model.MaCV == null)
            {
                model.MaCV = Session["cv"].ToString();
            }
            if (model.MaPB == null)
            {
                model.MaPB = Session["pb"].ToString();
            }
            if (model.GioiTinh == null)
            {
                model.GioiTinh = Session["gt"].ToString();
            }
            if (model.TrinhDoHocVan == null)
            {
                model.TrinhDoHocVan = Session["hv"].ToString();
            }
            if (model.BacLuong == null)
            {
                model.BacLuong = Session["bl"].ToString();
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

        //Phòng Ban
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

        //Chức Vụ
        public ActionResult DsCV()
        {
            if (Session["user"] == null)
            {
                return Redirect("~/Login/Login");
            }
            else
            {
                List<CHUCVU> cv = db.CHUCVU.ToList();
                return View(cv);
            }
        }

        public ActionResult ThemCV()
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
        public ActionResult ThemCV(CHUCVU model)
        {
            var cv = db.CHUCVU.Find(model.MaCV);
            if(cv != null)
            {
                ViewBag.CV = "yes";
                return View(model);
            }    
            else
            {
                db.CHUCVU.Add(model);
                db.SaveChanges();
                return RedirectToAction("DsCV");
            }    
        }

        public ActionResult CapNhatCV(string id)
        {
            if (Session["user"] == null)
            {
                return Redirect("~/Login/Login");
            }
            else
            {
                var cv = db.CHUCVU.Find(id);
                if (cv == null)
                {
                    return RedirectToAction("DsCV");
                }
                else
                {
                    return View(cv);
                }
            }
        }

        [HttpPost]
        public ActionResult CapNhatCV(CHUCVU model)
        {
            var cv = db.CHUCVU.Find(model.MaCV);
            if(cv == null)
            {
                return View(model);
            }    
            else
            {
                cv.TenCV = model.TenCV;
                db.SaveChanges();
                return RedirectToAction("DsCV");
            }    
        }

        public ActionResult XoaCV(string id)
        {
            var cv = db.CHUCVU.Find(id);
            if(cv == null)
            {
                return RedirectToAction("DsCV");
            }    
            else
            {
                db.CHUCVU.Remove(cv); 
                db.SaveChanges();
                return RedirectToAction("DsCV");
            }    
        }

        //Hợp Đồng Lao Động
        public ActionResult DsHD()
        {
            if (Session["user"] == null)
            {
                return Redirect("~/Login/Login");
            }
            else
            {
                List<HOPDONGLAODONG> cv = db.HOPDONGLAODONG.ToList();
                return View(cv);
            }
        }

        public ActionResult ThemHD()
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
        public ActionResult ThemHD(HOPDONGLAODONG model)
        {
            var cv = db.HOPDONGLAODONG.Find(model.MaHD);
            if (cv != null)
            {
                ViewBag.HD = "yes";
                return View(model);
            }
            else
            {
                db.HOPDONGLAODONG.Add(model);
                db.SaveChanges();
                return RedirectToAction("DsHD");
            }
        }

        public ActionResult CapNhatHD(string id)
        {
            if (Session["user"] == null)
            {
                return Redirect("~/Login/Login");
            }
            else
            {
                var cv = db.HOPDONGLAODONG.Find(id);
                if (cv == null)
                {
                    return RedirectToAction("DsHD");
                }
                else
                {
                    return View(cv);
                }
            }
        }

        [HttpPost]
        public ActionResult CapNhatHD(HOPDONGLAODONG model)
        {
            var cv = db.HOPDONGLAODONG.Find(model.MaHD);
            if (cv == null)
            {
                return View(model);
            }
            else
            {
                cv.LoaiHD = model.LoaiHD;
                cv.MaNV = model.MaNV;
                cv.NgayKy = model.NgayKy;
                cv.NgayHetHan = model.NgayHetHan;
                db.SaveChanges();
                return RedirectToAction("DsHD");
            }
        }

        public ActionResult XoaHD(string id)
        {
            var cv = db.HOPDONGLAODONG.Find(id);
            if (cv == null)
            {
                return RedirectToAction("DsHD");
            }
            else
            {
                db.HOPDONGLAODONG.Remove(cv);
                db.SaveChanges();
                return RedirectToAction("DsHD");
            }
        }

        //Công
        public ActionResult TraCuuDsCong()
        {
            if (Session["user"] == null)
            {
                return Redirect("~/Login/Login");
            }
            else
            {
                if (ViewBag.tc == null)
                {
                    ViewBag.df = "ok";
                }
                else
                {
                    ViewBag.df = "not";
                }
                List<CONG> cong = new List<CONG>();
                foreach (var item in db.CONG.ToList())
                {
                    if (item.Thang == DateTime.Now.Month && item.Nam == DateTime.Now.Year)
                    {
                        cong.Add(item);
                    }
                }
                return View(cong);
            }
        }

        public ActionResult DsCong(int? thang, int? nam)
        {
            if (Session["user"] == null)
            {
                return Redirect("~/Login/Login");
            }
            else
            {
                if (thang == null && nam == null)
                {
                    return RedirectToAction("TraCuuDsCong");
                }
                else
                {
                    if (thang == null || nam == null)
                    {
                        List<CONG> n = new List<CONG>();
                        ViewBag.err = "Phải nhập tháng và năm cần tra cứu";
                        return View(n);
                    }
                    else
                    {
                        List<CONG> congTheoThang = new List<CONG>();
                        foreach (var cong in db.CONG.ToList())
                        {
                            if (cong.Thang == thang && cong.Nam == nam)
                            {
                                congTheoThang.Add(cong);
                            }
                        }
                        ViewBag.err = null;
                        if (congTheoThang.Count > 0)
                        {
                            ViewBag.tc = "ok";
                        }
                        else
                        {
                            ViewBag.tc = "ok";
                        }
                        return View(congTheoThang);
                    }
                }
            }
        }

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
        public ActionResult TongHopCong(int thang, int nam, CONG model)
        {
            var a = db.CONG.FirstOrDefault(m => m.MaNV == model.MaNV && m.Thang == model.Thang && m.Nam == model.Nam);
            if(a != null)
            {
                db.CONG.Remove(a);
                var nv = db.ChamCong.FirstOrDefault(m => m.Thang == thang && m.Nam == nam);
                if (nv != null)
                {
                    foreach (var i in db.NHANVIEN.ToList())
                    {
                        List<ChamCong> dsCong = new List<ChamCong>();
                        foreach (var item in db.ChamCong.ToList())
                        {
                            if (item.MaNV == i.MaNV && item.Thang == thang && item.Nam == nam && item.Vao != null && item.Ra != null)
                            {
                                dsCong.Add(item);
                            }
                        }
                        model.MaNV = i.MaNV;
                        model.SoNgayCong = dsCong.Count;
                        model.SoNgayNghi = DateTime.DaysInMonth(nam, thang) - model.SoNgayCong;
                        model.Thang = thang;
                        model.Nam = nam;
                        db.CONG.Add(model);
                    }
                    db.SaveChanges();
                    return RedirectToAction("TraCuuDsCong");
                }
                else
                {
                    return View();
                }
            }    
            else
            {
                var nv = db.ChamCong.FirstOrDefault(m => m.Thang == thang && m.Nam == nam);
                if (nv != null)
                {
                    foreach (var i in db.NHANVIEN.ToList())
                    {
                        List<ChamCong> dsCong = new List<ChamCong>();
                        foreach (var item in db.ChamCong.ToList())
                        {
                            if (item.MaNV == i.MaNV && item.Thang == thang && item.Nam == nam && item.Vao != null && item.Ra != null)
                            {
                                dsCong.Add(item);
                            }
                        }
                        model.MaNV = i.MaNV;
                        model.SoNgayCong = dsCong.Count;
                        model.SoNgayNghi = DateTime.DaysInMonth(nam, thang) - model.SoNgayCong;
                        model.Thang = thang;
                        model.Nam = nam;
                        db.CONG.Add(model);
                    }
                    db.SaveChanges();
                    return RedirectToAction("TraCuuDsCong");
                }
                else
                {
                    return View();
                }
            }    
        }



        //Lương
        public ActionResult DsLuong()
        {
            if (Session["user"] == null)
            {
                return Redirect("~/Login/Login");
            }
            else
            {
                List<HOADONLUONG> dsLuong = new List<HOADONLUONG>();
                foreach (var item in db.HOADONLUONG.ToList())
                {
                    if (item.Thang == DateTime.Now.Month && item.Nam == DateTime.Now.Year)
                    {
                        dsLuong.Add(item);
                    }
                }
                return View(dsLuong);
            }
        }

        public ActionResult TraCuuLuong(int? thang, int? nam)
        {
            if(thang == null && nam == null)
            {
                return RedirectToAction("DsLuong");
            }    
            else
            {
                if (thang == null || nam == null)
                {
                    List<HOADONLUONG> n = new List<HOADONLUONG>();
                    ViewBag.er = "Phải nhập tháng và năm cần tra cứu";
                    return View(n);
                }
                else
                {
                    List<HOADONLUONG> luongTheoThang = new List<HOADONLUONG>();
                    foreach (var luong in db.HOADONLUONG.ToList())
                    {
                        if (luong.Thang == thang && luong.Nam == nam)
                        {
                            luongTheoThang.Add(luong);
                        }
                    }
                    ViewBag.er = null;
                    if (luongTheoThang.Count > 0)
                    {
                        ViewBag.tc = "ok";
                    }
                    else
                    {
                        ViewBag.tc = "not";
                    }
                    return View(luongTheoThang);
                }
            }    
        }

        public float HsPhuCap(int soNgayCong, int soNgayNghi, int thang, int nam, string id)
        {
            float hsPhuCap = 0;
            List<ChamCong> dsCong = new List<ChamCong>();
            foreach(var i in db.ChamCong.ToList())
            {
                if(i.MaNV == id && i.Thang == thang && i.Nam == nam) 
                {
                    dsCong.Add(i);
                }
            }
            if (soNgayNghi >= (DateTime.DaysInMonth(nam,thang)*0.2))
            {
                hsPhuCap =(float) 0;
            }
            else
            {
                List<ChamCong> ds = new List<ChamCong>();
                foreach (var item in dsCong)
                {
                    DateTime dateTime = new DateTime(item.Nam,item.Thang, item.Ngay);
                    if(dateTime.DayOfWeek == DayOfWeek.Sunday)
                    {
                        ds.Add(item);
                    }
                }
                if(ds.Count > 3 || soNgayCong >= (DateTime.DaysInMonth(nam, thang) * 0.8)) 
                {
                    hsPhuCap=(float) 0.5;
                }
            }
            return (float)hsPhuCap;
        }

        public ActionResult TongHopLuong()
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
        public ActionResult TongHopLuong(int thang, int nam, HOADONLUONG model)
        {
            var a = db.HOADONLUONG.FirstOrDefault(m => m.MaNV == model.MaNV && m.Thang == model.Thang && m.Nam == model.Nam);
            if(a != null)
            {
                db.HOADONLUONG.Remove(a);
                var nv = db.CONG.FirstOrDefault(m => m.Thang == thang && m.Nam == nam);
                if (nv != null)
                {
                    List<CONG> ds = new List<CONG>();
                    foreach (var item in db.CONG.ToList())
                    {
                        if (item.Thang == thang && item.Nam == nam)
                        {
                            ds.Add(item);
                        }
                    }
                    foreach (var item in ds)
                    {
                        model.BacLuong = item.NHANVIEN.BacLuong;
                        model.MaNV = item.MaNV;
                        model.HSPhuCap = HsPhuCap((int)item.SoNgayCong, (int)item.SoNgayNghi, item.Thang, item.Nam, item.MaNV);
                        var luongCB = db.LUONG.Find(model.BacLuong);
                        var luongThoaThuan = luongCB.LuongCoBan + luongCB.LuongCoBan * model.HSPhuCap;
                        model.ThucLinh = (luongThoaThuan / 26) * item.SoNgayCong;
                        db.HOADONLUONG.Add(model);
                    }
                    db.SaveChanges();
                    return RedirectToAction("DsLuong");
                }
                else
                {
                    return View();
                }
            } 
            else
            {
                var nv = db.CONG.FirstOrDefault(m => m.Thang == thang && m.Nam == nam);
                if (nv != null)
                {
                    List<CONG> ds = new List<CONG>();
                    foreach (var item in db.CONG.ToList())
                    {
                        if (item.Thang == thang && item.Nam == nam)
                        {
                            ds.Add(item);
                        }
                    }
                    foreach (var item in ds)
                    {
                        model.BacLuong = item.NHANVIEN.BacLuong;
                        model.MaNV = item.MaNV;
                        model.HSPhuCap = HsPhuCap((int)item.SoNgayCong, (int)item.SoNgayNghi, item.Thang, item.Nam, item.MaNV);
                        var luongCB = db.LUONG.Find(model.BacLuong);
                        var luongThoaThuan = luongCB.LuongCoBan + luongCB.LuongCoBan * model.HSPhuCap;
                        model.ThucLinh = (luongThoaThuan / 26) * item.SoNgayCong;
                        db.HOADONLUONG.Add(model);
                    }
                    db.SaveChanges();
                    return RedirectToAction("DsLuong");
                }
                else
                {
                    return View();
                }
            }    
        }

        //Tìm Kiếm
        public ActionResult TimKiem(string tuKhoa)
        {
            if (Session["user"] == null)
            {
                return Redirect("~/Login/Login");
            }
            else
            {
                if (tuKhoa == null)
                {
                    return RedirectToAction("DanhSachNV");
                }
                else
                {
                    List<NHANVIEN> nv = new List<NHANVIEN>();
                    foreach (var item in db.NHANVIEN.ToList())
                    {
                        if (item.HoTenNV != null)
                        {
                            if (item.HoTenNV.ToLower().Contains(tuKhoa.ToLower())) nv.Add(item);
                        }
                    }
                    return View(nv);
                }
            } 
        }
    }
}