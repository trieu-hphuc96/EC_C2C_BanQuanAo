using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EC_C2C_BanQuanAo.Areas.NguoiBan.Controllers
{
    public class HoaDon_BanHangController : Controller
    {
        private EC_C2C_BanQuanAoDBContext db = new EC_C2C_BanQuanAoDBContext();

        // GET: NguoiBan/HoaDon_BanHang
        public ActionResult CapNhatTinhTrang()
        {
            //var hdbh = db.GioHangs.ToList();


            ViewBag.TrangThai = new List<SelectListItem>()
            {
                new SelectListItem() {Text="Đang chờ xử lý", Value="1"},
                new SelectListItem() {Text="Đang giao hàng", Value="2"},
                new SelectListItem() {Text="Đã hoàn tất", Value="3"},
                new SelectListItem() {Text="Đã hủy", Value="0"},
            };

            return View();
        }
    }
}