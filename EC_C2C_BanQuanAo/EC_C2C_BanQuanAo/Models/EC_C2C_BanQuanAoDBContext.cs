namespace EC_C2C_BanQuanAo
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class EC_C2C_BanQuanAoDBContext : DbContext
    {
        public EC_C2C_BanQuanAoDBContext()
            : base("name=EC_C2C_BanQuanAoDBContext")
        {
        }

        public virtual DbSet<CT_HoaDon_BanHang> CT_HoaDon_BanHang { get; set; }
        public virtual DbSet<DanhGia> DanhGias { get; set; }
        public virtual DbSet<GioHang> GioHangs { get; set; }
        public virtual DbSet<GoiTin> GoiTins { get; set; }
        public virtual DbSet<HoaDon_BanHang> HoaDon_BanHang { get; set; }
        public virtual DbSet<HoaDon_BanTin> HoaDon_BanTin { get; set; }
        public virtual DbSet<LoaiTK> LoaiTKs { get; set; }
        public virtual DbSet<TaiKhoan> TaiKhoans { get; set; }
        public virtual DbSet<Tin> Tins { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GoiTin>()
                .HasMany(e => e.HoaDon_BanTin)
                .WithRequired(e => e.GoiTin)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<HoaDon_BanHang>()
                .HasMany(e => e.CT_HoaDon_BanHang)
                .WithRequired(e => e.HoaDon_BanHang)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<LoaiTK>()
                .HasMany(e => e.TaiKhoans)
                .WithOptional(e => e.LoaiTK1)
                .HasForeignKey(e => e.LoaiTK);

            modelBuilder.Entity<TaiKhoan>()
                .Property(e => e.TenNguoiDung)
                .IsUnicode(false);

            modelBuilder.Entity<TaiKhoan>()
                .Property(e => e.MatKhau)
                .IsUnicode(false);

            modelBuilder.Entity<TaiKhoan>()
                .Property(e => e.DiemDanhGia)
                .HasPrecision(7, 1);

            modelBuilder.Entity<TaiKhoan>()
                .HasMany(e => e.DanhGias)
                .WithRequired(e => e.TaiKhoan)
                .HasForeignKey(e => e.MaTK_Ban)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TaiKhoan>()
                .HasMany(e => e.DanhGias1)
                .WithRequired(e => e.TaiKhoan1)
                .HasForeignKey(e => e.MaTK_Mua)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TaiKhoan>()
                .HasMany(e => e.GioHangs)
                .WithRequired(e => e.TaiKhoan)
                .HasForeignKey(e => e.MaTK_Ban)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TaiKhoan>()
                .HasMany(e => e.GioHangs1)
                .WithRequired(e => e.TaiKhoan1)
                .HasForeignKey(e => e.MaTK_Mua)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TaiKhoan>()
                .HasMany(e => e.HoaDon_BanHang)
                .WithRequired(e => e.TaiKhoan)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TaiKhoan>()
                .HasMany(e => e.HoaDon_BanTin)
                .WithRequired(e => e.TaiKhoan)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TaiKhoan>()
                .HasMany(e => e.Tins)
                .WithRequired(e => e.TaiKhoan)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Tin>()
                .HasMany(e => e.CT_HoaDon_BanHang)
                .WithRequired(e => e.Tin)
                .WillCascadeOnDelete(false);
        }
    }
}
