namespace NivelAccesDate_ORM_CodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tests_update_foreign_key_recheck : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Images", new[] { "UserId_UserId1" });
            DropColumn("dbo.Images", "UserId_UserId");
            RenameColumn(table: "dbo.Images", name: "UserId_UserId1", newName: "UserId_UserId");
            AlterColumn("dbo.Images", "UserId_UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Images", "UserId_UserId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Images", new[] { "UserId_UserId" });
            AlterColumn("dbo.Images", "UserId_UserId", c => c.String());
            RenameColumn(table: "dbo.Images", name: "UserId_UserId", newName: "UserId_UserId1");
            AddColumn("dbo.Images", "UserId_UserId", c => c.String());
            CreateIndex("dbo.Images", "UserId_UserId1");
        }
    }
}
