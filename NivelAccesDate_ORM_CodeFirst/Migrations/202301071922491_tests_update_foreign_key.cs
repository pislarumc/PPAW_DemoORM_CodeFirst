namespace NivelAccesDate_ORM_CodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tests_update_foreign_key : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Images", "UserId_UserId", "dbo.Users");
            DropIndex("dbo.Images", new[] { "UserId_UserId" });
            AddColumn("dbo.Images", "UserId_UserId1", c => c.String(maxLength: 128));
            AlterColumn("dbo.Images", "UserId_UserId", c => c.String());
            CreateIndex("dbo.Images", "UserId_UserId1");
            AddForeignKey("dbo.Images", "UserId_UserId1", "dbo.Users", "UserId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Images", "UserId_UserId1", "dbo.Users");
            DropIndex("dbo.Images", new[] { "UserId_UserId1" });
            AlterColumn("dbo.Images", "UserId_UserId", c => c.String(maxLength: 128));
            DropColumn("dbo.Images", "UserId_UserId1");
            CreateIndex("dbo.Images", "UserId_UserId");
            AddForeignKey("dbo.Images", "UserId_UserId", "dbo.Users", "UserId");
        }
    }
}
