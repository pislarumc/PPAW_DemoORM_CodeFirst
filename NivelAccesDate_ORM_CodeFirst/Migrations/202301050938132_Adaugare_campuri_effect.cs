namespace NivelAccesDate_ORM_CodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Adaugare_campuri_effect : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Effects", "CssProperty", c => c.String());
            AddColumn("dbo.Effects", "PropertyValue", c => c.String());
            AddColumn("dbo.Effects", "PropertyRangeMin", c => c.String());
            AddColumn("dbo.Effects", "PropertyRangeMax", c => c.String());
            AddColumn("dbo.Effects", "PropertyUnit", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Effects", "PropertyUnit");
            DropColumn("dbo.Effects", "PropertyRangeMax");
            DropColumn("dbo.Effects", "PropertyRangeMin");
            DropColumn("dbo.Effects", "PropertyValue");
            DropColumn("dbo.Effects", "CssProperty");
        }
    }
}
