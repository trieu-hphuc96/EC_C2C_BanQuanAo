namespace EC_C2C_BanQuanAo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class HoaDon_BanTin
    {
        [Key]
        public int MaHDT { get; set; }

        public int MaTK { get; set; }

        public int MaGoi { get; set; }

        public int? TinKhuyenMai { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Ngay { get; set; }

        public virtual GoiTin GoiTin { get; set; }

        public virtual TaiKhoan TaiKhoan { get; set; }
    }
}
