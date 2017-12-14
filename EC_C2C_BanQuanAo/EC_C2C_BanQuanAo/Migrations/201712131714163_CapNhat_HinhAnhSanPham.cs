namespace EC_C2C_BanQuanAo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CapNhat_HinhAnhSanPham : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.HinhAnhSanPham", "MaTin");
            AddForeignKey("dbo.HinhAnhSanPham", "MaTin", "dbo.Tin", "MaTin");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.HinhAnhSanPham", "MaTin", "dbo.Tin");
            DropIndex("dbo.HinhAnhSanPham", new[] { "MaTin" });
        }
    }
}
