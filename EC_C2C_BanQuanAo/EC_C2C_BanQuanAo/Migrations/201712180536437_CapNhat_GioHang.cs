namespace EC_C2C_BanQuanAo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CapNhat_GioHang : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.GioHang", "MaTin");
            AddForeignKey("dbo.GioHang", "MaTin", "dbo.Tin", "MaTin", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GioHang", "MaTin", "dbo.Tin");
            DropIndex("dbo.GioHang", new[] { "MaTin" });
        }
    }
}
