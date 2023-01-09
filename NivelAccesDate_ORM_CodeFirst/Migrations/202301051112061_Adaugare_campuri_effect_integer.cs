namespace NivelAccesDate_ORM_CodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Adaugare_campuri_effect_integer : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Effects", "PropertyValue", c => c.Int(nullable: false));
            AlterColumn("dbo.Effects", "PropertyRangeMin", c => c.Int(nullable: false));
            AlterColumn("dbo.Effects", "PropertyRangeMax", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Effects", "PropertyRangeMax", c => c.String());
            AlterColumn("dbo.Effects", "PropertyRangeMin", c => c.String());
            AlterColumn("dbo.Effects", "PropertyValue", c => c.String());
        }
    }
}
