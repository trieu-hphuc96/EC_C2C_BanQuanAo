namespace EC_C2C_BanQuanAo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TaiKhoan")]
    public partial class TaiKhoan
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TaiKhoan()
        {
            DanhGias = new HashSet<DanhGia>();
            DanhGias1 = new HashSet<DanhGia>();
            GioHangs = new HashSet<GioHang>();
            GioHangs1 = new HashSet<GioHang>();
            HoaDon_BanHang = new HashSet<HoaDon_BanHang>();
            HoaDon_BanTin = new HashSet<HoaDon_BanTin>();
            Tins = new HashSet<Tin>();

            XacNhanEmail = 0;
        }

        [Key]
        public int MaTK { get; set; }

        public int? LoaiTK { get; set; }

        [Display(Name = "Tên đăng nhập")]
        [StringLength(30)]
        public string TenNguoiDung { get; set; }

        [Display(Name = "Mật khẩu")]
        public string MatKhau { get; set; }

        [Display(Name = "Họ và Tên")]
        [StringLength(50)]
        public string TenDayDu { get; set; }

        [Display(Name = "Địa chỉ")]
        [StringLength(50)]
        public string DiaChi { get; set; }

        [Display(Name = "Email")]
        [StringLength(50)]
        public string Email { get; set; }

        public int XacNhanEmail { get; set; }

        [Display(Name = "Ngày sinh")]
        [Column(TypeName = "date")]
        public DateTime? NgaySinh { get; set; }

        [Display(Name = "Số điện thoại")]
        [StringLength(15)]
        public string SDT { get; set; }

        [Display(Name = "CMND")]
        [StringLength(20)]
        public string CMND { get; set; }

        [Display(Name = "Ngày đăng ký")]
        [Column(TypeName = "date")]
        public DateTime? NgayDangKy { get; set; }

        [Display(Name = "Ngày đánh giá")]
        [Column(TypeName = "date")]
        public DateTime? NgayDanhGia { get; set; }

        public int? TongTinDaMua { get; set; }

        public int? TongTinConLai { get; set; }

        public int? TongTinKM { get; set; }

        public decimal? DiemDanhGia { get; set; }

        public int? TinhTrang { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DanhGia> DanhGias { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DanhGia> DanhGias1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GioHang> GioHangs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GioHang> GioHangs1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HoaDon_BanHang> HoaDon_BanHang { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HoaDon_BanTin> HoaDon_BanTin { get; set; }

        public virtual LoaiTK LoaiTK1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tin> Tins { get; set; }

        public string[] Roles { get; set; }

        public string VCode { get; set; }
    }
}
