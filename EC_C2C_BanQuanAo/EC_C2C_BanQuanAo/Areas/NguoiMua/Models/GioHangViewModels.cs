using EC_C2C_BanQuanAo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EC_C2C_BanQuanAo.Areas.NguoiMua.Models
{
    public class GioHangViewModels
    {
    }

    public class TinhTienViewModel
    {
        public GioHang GioHang { get; set; }

        public HinhAnhSanPham HinhAnh { get; set; }
    }
}