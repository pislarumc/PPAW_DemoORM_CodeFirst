namespace NivelAccesDate_ORM_CodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DataBaseCodeFirstV_1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        ImageId = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Path = c.String(),
                        Dimensions = c.String(),
                        UserId_UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ImageId)
                .ForeignKey("dbo.Users", t => t.UserId_UserId)
                .Index(t => t.UserId_UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        UserName = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                        Role = c.String(),
                        Subscribed = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Images", "UserId_UserId", "dbo.Users");
            DropIndex("dbo.Images", new[] { "UserId_UserId" });
            DropTable("dbo.Users");
            DropTable("dbo.Images");
        }
    }
}
