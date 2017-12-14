using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EC_C2C_BanQuanAo.Areas.NguoiBan.Models
{
    public class SanPhamViewModels
    {
    }

    public class DangTinSanPhamViewModel
    {
        [Key]
        public int MaTin { get; set; }

        public int MaTK { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập {0}")]
        [Display(Name = "Mã SKU")]
        [StringLength(50)]
        public string MaSKU { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập {0}")]
        [Display(Name = "Tên sản phẩm")]
        [StringLength(50)]
        public string TenSP { get; set; }


        [Display(Name = "Mô tả")]
        public string MoTa { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập {0}")]
        [Display(Name = "Số lượng")]
        public int? SoLuong { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập {0}")]
        [Display(Name = "Giá")]
        public int? Gia { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập {0}")]
        [Display(Name = "Ngày đăng")]
        [Column(TypeName = "date")]
        public DateTime? NgayDang { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập {0}")]
        [Display(Name = "Ngày kết thúc")]
        [Column(TypeName = "date")]
        public DateTime? NgayKetThuc { get; set; }

        [Display(Name = "Hình ảnh")]
        public ICollection<HttpPostedFileBase> HinhAnhs { get; set; }
    }
}