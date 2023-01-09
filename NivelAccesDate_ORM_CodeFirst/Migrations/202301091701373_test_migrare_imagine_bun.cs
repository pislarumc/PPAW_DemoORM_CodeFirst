namespace NivelAccesDate_ORM_CodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test_migrare_imagine_bun : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Histories", name: "Effect_EffectId", newName: "EffectId");
            RenameColumn(table: "dbo.Histories", name: "Image_ImageId", newName: "ImageId");
            RenameColumn(table: "dbo.Histories", name: "User_UserId", newName: "UserId");
            RenameColumn(table: "dbo.Images", name: "UserId_UserId", newName: "UserId");
            RenameIndex(table: "dbo.Histories", name: "IX_User_UserId", newName: "IX_UserId");
            RenameIndex(table: "dbo.Histories", name: "IX_Effect_EffectId", newName: "IX_EffectId");
            RenameIndex(table: "dbo.Histories", name: "IX_Image_ImageId", newName: "IX_ImageId");
            RenameIndex(table: "dbo.Images", name: "IX_UserId_UserId", newName: "IX_UserId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Images", name: "IX_UserId", newName: "IX_UserId_UserId");
            RenameIndex(table: "dbo.Histories", name: "IX_ImageId", newName: "IX_Image_ImageId");
            RenameIndex(table: "dbo.Histories", name: "IX_EffectId", newName: "IX_Effect_EffectId");
            RenameIndex(table: "dbo.Histories", name: "IX_UserId", newName: "IX_User_UserId");
            RenameColumn(table: "dbo.Images", name: "UserId", newName: "UserId_UserId");
            RenameColumn(table: "dbo.Histories", name: "UserId", newName: "User_UserId");
            RenameColumn(table: "dbo.Histories", name: "ImageId", newName: "Image_ImageId");
            RenameColumn(table: "dbo.Histories", name: "EffectId", newName: "Effect_EffectId");
        }
    }
}
