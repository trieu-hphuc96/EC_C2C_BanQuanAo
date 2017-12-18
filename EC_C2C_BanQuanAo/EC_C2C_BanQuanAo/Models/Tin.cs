namespace EC_C2C_BanQuanAo
{
    using EC_C2C_BanQuanAo.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Tin")]
    public partial class Tin
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tin()
        {
            CT_HoaDon_BanHang = new HashSet<CT_HoaDon_BanHang>();
            HinhAnhSanPham = new HashSet<HinhAnhSanPham>();
        }

        [Key]
        public int MaTin { get; set; }

        public int MaTK { get; set; }

        [StringLength(50)]
        public string MaSKU { get; set; }

        [StringLength(50)]
        public string TenSP { get; set; }

        public string MoTa { get; set; }

        public int? SoLuong { get; set; }

        public int? Gia { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayDang { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayKetThuc { get; set; }

        public int? TinhTrang { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CT_HoaDon_BanHang> CT_HoaDon_BanHang { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HinhAnhSanPham> HinhAnhSanPham { get; set; }

        public virtual TaiKhoan TaiKhoan { get; set; }
    }
}
