namespace EC_C2C_BanQuanAo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("GoiTin")]
    public partial class GoiTin
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public GoiTin()
        {
            HoaDon_BanTin = new HashSet<HoaDon_BanTin>();
        }

        [Key]
        public int MaGoi { get; set; }

        [StringLength(50)]
        public string TenGoi { get; set; }

        public int? SoLuongTin { get; set; }

        public int? Gia { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HoaDon_BanTin> HoaDon_BanTin { get; set; }
    }
}
