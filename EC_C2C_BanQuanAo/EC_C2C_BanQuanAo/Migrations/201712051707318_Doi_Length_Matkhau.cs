namespace EC_C2C_BanQuanAo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Doi_Length_Matkhau : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TaiKhoan", "MatKhau", c => c.String(unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TaiKhoan", "MatKhau", c => c.String(maxLength: 20, unicode: false));
        }
    }
}
