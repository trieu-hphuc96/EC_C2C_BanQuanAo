namespace EC_C2C_BanQuanAo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CT_HoaDon_BanHang
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaHDH { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaTin { get; set; }

        public int? Gia { get; set; }

        public int? SoLuong { get; set; }

        public virtual HoaDon_BanHang HoaDon_BanHang { get; set; }

        public virtual Tin Tin { get; set; }
    }
}
