using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EC_C2C_BanQuanAo.Areas.NguoiBan.Models
{
    public class HoaDon_BanHangViewModels
    {
    }

    public class CapNhatTinhTrangViewModel
    {
        public HoaDon_BanHang HoaDon_BanHang { get; set; }

        public List<SelectListItem> DanhSachTrangThai { get; set; }
    }

    public class DanhSachTinhTrang
    {
        public int MaHDH { get; set; }

        public int TrangThai { get; set; }
    }
}