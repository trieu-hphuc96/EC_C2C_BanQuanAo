using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using EC_C2C_BanQuanAo.Areas.NguoiBan.Models;

namespace EC_C2C_BanQuanAo.Areas.NguoiBan.Controllers
{
    public class HoaDon_BanHangController : Controller
    {
        private EC_C2C_BanQuanAoDBContext db = new EC_C2C_BanQuanAoDBContext();

        // GET: NguoiBan/HoaDon_BanHang
        public ActionResult CapNhatTinhTrang()
        {
            var hdbh = db.HoaDon_BanHang.Include(p => p.TaiKhoan).ToList();

            var cnttvms = new List<CapNhatTinhTrangViewModel>();

            foreach(var item in hdbh)
            {
                CapNhatTinhTrangViewModel cnttvm = new CapNhatTinhTrangViewModel();
                cnttvm.HoaDon_BanHang = item;
                cnttvm.DanhSachTrangThai = new List<SelectListItem>()
                {
                    new SelectListItem() {Text="Đang chờ xử lý", Value="1", Selected = (cnttvm.HoaDon_BanHang.TrangThai == 1)},
                    new SelectListItem() {Text="Đang giao hàng", Value="2", Selected = (cnttvm.HoaDon_BanHang.TrangThai == 2)},
                    new SelectListItem() {Text="Đã hoàn tất", Value="3", Selected = (cnttvm.HoaDon_BanHang.TrangThai == 3)},
                    new SelectListItem() {Text="Đã hủy", Value="0", Selected = (cnttvm.HoaDon_BanHang.TrangThai == 0)},
                };
                
                cnttvms.Add(cnttvm);
            }

            return View(cnttvms);
        }

        [HttpPost]
        public ActionResult CapNhatTinhTrang(int? TrangThai, HoaDon_BanHang hdbhh)
        {
            var hdbh = db.HoaDon_BanHang.Include(p => p.TaiKhoan).ToList();

            ViewBag.TrangThai = new List<SelectListItem>()
            {
                new SelectListItem() {Text="Đang chờ xử lý", Value="1"},
                new SelectListItem() {Text="Đang giao hàng", Value="2"},
                new SelectListItem() {Text="Đã hoàn tất", Value="3"},
                new SelectListItem() {Text="Đã hủy", Value="0"},
            };

            return View(hdbh);
        }
    }
}