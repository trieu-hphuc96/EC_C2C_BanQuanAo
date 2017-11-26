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

        [Key]
        public int MaHDH { get; set; }

        public int MaTK { get; set; }

        [StringLength(30)]
        public string DuongSo { get; set; }

        [StringLength(30)]
        public string QuanHuyen { get; set; }

        [StringLength(30)]
        public string TinhThanh { get; set; }

        public int? SoLuong { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Ngay { get; set; }

        public int? TongTien { get; set; }

        public int? TrangThai { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CT_HoaDon_BanHang> CT_HoaDon_BanHang { get; set; }

        public virtual TaiKhoan TaiKhoan { get; set; }
    }
}
