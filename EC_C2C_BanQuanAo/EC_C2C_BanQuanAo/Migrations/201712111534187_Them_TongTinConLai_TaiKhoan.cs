namespace EC_C2C_BanQuanAo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Them_TongTinConLai_TaiKhoan : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TaiKhoan", "TongTinConLai", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TaiKhoan", "TongTinConLai");
        }
    }
}
