namespace EC_C2C_BanQuanAo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Them_XacNhanEmail_TaiKhoan : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TaiKhoan", "XacNhanEmail", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TaiKhoan", "XacNhanEmail");
        }
    }
}
