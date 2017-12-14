using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EC_C2C_BanQuanAo.Areas.NguoiBan.Models
{
    public class DangKyViewModels
    {
        [Required(ErrorMessage = "Vui lòng nhập {0}")]
        [Display(Name = "Tên đăng nhập")]
        [StringLength(30)]
        public string TenNguoiDung { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập {0}")]
        [StringLength(20, ErrorMessage = "{0} chỉ dài tối thiểu {2} ký tự.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        public string MatKhau { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Xác nhận Mật khẩu")]
        [Compare("MatKhau", ErrorMessage = "Mật khẩu và Xác nhận mật khẩu không khớp.")]
        public string XacNhanMatKhau { get; set; }

        [Display(Name = "Họ và Tên")]
        [StringLength(50)]
        public string TenDayDu { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập {0}")]
        [EmailAddress]
        [Display(Name = "Email")]
        [StringLength(50)]
        public string Email { get; set; }
    }

    public class DangNhapViewModels
    {
        public int MaTK;

        [Required(ErrorMessage = "Vui lòng nhập {0}")]
        [Display(Name = "Tên đăng nhập")]
        public string TenNguoiDung { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập {0}")]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        public string MatKhau { get; set; }
    } 
}