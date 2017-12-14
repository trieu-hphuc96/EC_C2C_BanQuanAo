namespace EC_C2C_BanQuanAo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Them_HinhAnhSanPham : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HinhAnhSanPham",
                c => new
                    {
                        MaTin = c.Int(nullable: false),
                        MaHinh = c.Int(nullable: false),
                        DuongDan = c.String(),
                    })
                .PrimaryKey(t => new { t.MaTin, t.MaHinh });
            
        }
        
        public override void Down()
        {
            DropTable("dbo.HinhAnhSanPham");
        }
    }
}
