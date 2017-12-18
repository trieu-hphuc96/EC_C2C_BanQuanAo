using EC_C2C_BanQuanAo.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EC_C2C_BanQuanAo.Areas.Admin.Controllers
{
    public class TaiKhoan_AdminController : Controller
    {
        EC_C2C_BanQuanAoDBContext db = new EC_C2C_BanQuanAoDBContext();

        public ActionResult TrangChu()
        {
            return View("TrangChu");
        }

        public ActionResult DanhGiaNguoiBan()
        {
            var nguoiban = db.TaiKhoans.Where(m => m.LoaiTK == 3).ToList();

            var dgnbvms = new List<DanhGiaNguoiBanViewModel>();
            foreach (var item in nguoiban)
            {
                DanhGiaNguoiBanViewModel dgnbvm = new DanhGiaNguoiBanViewModel();
                dgnbvm.TaiKhoan_NguoiBan = item;
                dgnbvm.DanhSachTrangThai = new List<SelectListItem>()
                {
                    new SelectListItem() {Text="Đang chờ xử lý", Value="1", Selected = (dgnbvm.TaiKhoan_NguoiBan.TinhTrang == 1)},
                    new SelectListItem() {Text="Đang hoạt động", Value="2", Selected = (dgnbvm.TaiKhoan_NguoiBan.TinhTrang == 2)},
                    new SelectListItem() {Text="Ngừng hoạt động", Value="0", Selected = (dgnbvm.TaiKhoan_NguoiBan.TinhTrang == 0)},
                };


                dgnbvms.Add(dgnbvm);
            }

            return View(dgnbvms);
        }

        [HttpPost]
        public ActionResult DanhGiaNguoiBan(IEnumerable<DanhSachTinhTrang> dstt)
        {
            foreach (var item in dstt)
            {
                TaiKhoan hd = db.TaiKhoans.Find(item.MaTK);
                if (hd.TinhTrang == 0)
                {
                    hd.NgayDanhGia = DateTime.Now.AddDays(60);
                }
                hd.TinhTrang = item.TinhTrang;
                    

                db.Entry(hd).State = EntityState.Modified;
            }

            db.SaveChanges();
            ModelState.Clear();

            var nguoiban = db.TaiKhoans.Where(m => m.LoaiTK == 3).ToList();

            var dgnbvms = new List<DanhGiaNguoiBanViewModel>();

            foreach (var item in nguoiban)
            {
                DanhGiaNguoiBanViewModel dgnbvm = new DanhGiaNguoiBanViewModel();
                dgnbvm.TaiKhoan_NguoiBan = item;
                dgnbvm.DanhSachTrangThai = new List<SelectListItem>()
                {
                    new SelectListItem() {Text="Đang chờ xử lý", Value="1", Selected = (dgnbvm.TaiKhoan_NguoiBan.TinhTrang == 1)},
                    new SelectListItem() {Text="Đang hoạt động", Value="2", Selected = (dgnbvm.TaiKhoan_NguoiBan.TinhTrang == 2)},
                    new SelectListItem() {Text="Ngừng hoạt động", Value="0", Selected = (dgnbvm.TaiKhoan_NguoiBan.TinhTrang == 0)},
                };


                dgnbvms.Add(dgnbvm);
            }

            return View(dgnbvms);
        }

        public JsonResult layDanhSachTaiKhoan(int maTK)
        {
            var nguoiban = db.TaiKhoans.Where(mahd => mahd.MaTK == maTK).SingleOrDefault();

            var DanhSachTinhTrang = new DanhSachTinhTrang();

            DanhSachTinhTrang.MaTK = nguoiban.MaTK;
            DanhSachTinhTrang.TinhTrang = nguoiban.TinhTrang ?? 1;

            return Json(DanhSachTinhTrang, JsonRequestBehavior.AllowGet);
        }
    }
}