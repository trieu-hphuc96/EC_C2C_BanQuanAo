using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EC_C2C_BanQuanAo.Areas.Admin.Models
{
    public class TaiKhoan_AdminViewModels
    {
    }

    public class DanhGiaNguoiBanViewModel
    {
        public TaiKhoan TaiKhoan_NguoiBan { get; set; }

        public List<SelectListItem> DanhSachTrangThai { get; set; }
    }

    public class DanhSachTinhTrang
    {
        public int MaTK { get; set; }

        public int TinhTrang { get; set; }
    }
}