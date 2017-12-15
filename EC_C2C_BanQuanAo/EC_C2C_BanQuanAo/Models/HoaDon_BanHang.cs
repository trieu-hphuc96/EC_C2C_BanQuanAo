namespace EC_C2C_BanQuanAo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class HoaDon_BanHang
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HoaDon_BanHang()
        {
            CT_HoaDon_BanHang = new HashSet<CT_HoaDon_BanHang>();
        }

        [Display(Name = "Mã HDH")]
        [Key]
        public int MaHDH { get; set; }

        [Display(Name = "Mã người mua")]
        public int MaTK { get; set; }

        [Display(Name = "Số đường")]
        [StringLength(30)]
        public string DuongSo { get; set; }

        [Display(Name = "Quận huyện")]
        [StringLength(30)]
        public string QuanHuyen { get; set; }

        [Display(Name = "Tỉnh thành")]
        [StringLength(30)]
        public string TinhThanh { get; set; }

        [Display(Name = "Số lượng")]
        public int? SoLuong { get; set; }

        [Display(Name = "Ngày")]
        [Column(TypeName = "date")]
        public DateTime? Ngay { get; set; }

        [Display(Name = "Tổng tiền")]
        public int? TongTien { get; set; }

        [Display(Name = "Tình trạng")]
        public int? TrangThai { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CT_HoaDon_BanHang> CT_HoaDon_BanHang { get; set; }

        public virtual TaiKhoan TaiKhoan { get; set; }
    }
}
