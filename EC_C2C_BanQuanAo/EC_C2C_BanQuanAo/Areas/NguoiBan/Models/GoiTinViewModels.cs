using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EC_C2C_BanQuanAo.Areas.NguoiBan.Models
{
    public class MuaGoiTinViewModel
    {
        [Required]
        public int MaGoi { get; set; }

        [Display(Name = "Số tin")]
        public int? SoLuongTin { get; set; }

        [Display(Name = "Giá tiền")]
        public int? Gia { get; set; }

        [Display(Name = "Số tin đã mua")]
        public int SoTinDaMua { get; set; }

        [Display(Name = "Số tin còn lại")]
        public int SoTinConLai { get; set; }

        [Display(Name = "Khuyến mãi")]
        public int KhuyenMai { get; set; }
    }
}