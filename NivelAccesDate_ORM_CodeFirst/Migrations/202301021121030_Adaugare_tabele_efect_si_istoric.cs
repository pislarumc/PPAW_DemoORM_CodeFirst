namespace NivelAccesDate_ORM_CodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Adaugare_tabele_efect_si_istoric : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Effects",
                c => new
                    {
                        EffectId = c.String(nullable: false, maxLength: 128),
                        EffectName = c.String(),
                        Cost = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.EffectId);
            
            CreateTable(
                "dbo.Histories",
                c => new
                    {
                        HistoryId = c.String(nullable: false, maxLength: 128),
                        LoggingTime = c.DateTime(nullable: false),
                        Description = c.String(),
                        Effect_EffectId = c.String(maxLength: 128),
                        Image_ImageId = c.String(maxLength: 128),
                        User_UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.HistoryId)
                .ForeignKey("dbo.Effects", t => t.Effect_EffectId)
                .ForeignKey("dbo.Images", t => t.Image_ImageId)
                .ForeignKey("dbo.Users", t => t.User_UserId)
                .Index(t => t.Effect_EffectId)
                .Index(t => t.Image_ImageId)
                .Index(t => t.User_UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Histories", "User_UserId", "dbo.Users");
            DropForeignKey("dbo.Histories", "Image_ImageId", "dbo.Images");
            DropForeignKey("dbo.Histories", "Effect_EffectId", "dbo.Effects");
            DropIndex("dbo.Histories", new[] { "User_UserId" });
            DropIndex("dbo.Histories", new[] { "Image_ImageId" });
            DropIndex("dbo.Histories", new[] { "Effect_EffectId" });
            DropTable("dbo.Histories");
            DropTable("dbo.Effects");
        }
    }
}
