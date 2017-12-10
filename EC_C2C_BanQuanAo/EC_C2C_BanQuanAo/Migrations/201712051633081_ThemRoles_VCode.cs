namespace EC_C2C_BanQuanAo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ThemRoles_VCode : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TaiKhoan", "VCode", c => c.String());
            AddColumn("dbo.TaiKhoan", "Roles", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TaiKhoan", "VCode");
            DropColumn("dbo.TaiKhoan", "Roles");
        }
    }
}
