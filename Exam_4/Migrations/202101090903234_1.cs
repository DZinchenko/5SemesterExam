namespace Exam_4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Latitude = c.Int(nullable: false),
                        Longitude = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Doctors",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(maxLength: 128),
                        CityId = c.Int(nullable: false),
                        Specialization = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cities", t => t.CityId).Index(t => t.CityId);
            
            CreateTable(
                "dbo.Engineers",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(maxLength: 128),
                        CityId = c.Int(nullable: false),
                        FavoriteVideogame = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cities", t => t.CityId).Index(t => t.CityId);

        }
        
        public override void Down()
        {
            DropTable("dbo.Engineers");
            DropTable("dbo.Doctors");
            DropTable("dbo.Cities");
        }
    }
}
