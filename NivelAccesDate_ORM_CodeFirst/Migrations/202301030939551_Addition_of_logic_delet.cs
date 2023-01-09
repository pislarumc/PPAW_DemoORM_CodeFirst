namespace NivelAccesDate_ORM_CodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addition_of_logic_delet : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Effects", "Deleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Images", "Deleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Users", "Deleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "Deleted");
            DropColumn("dbo.Images", "Deleted");
            DropColumn("dbo.Effects", "Deleted");
        }
    }
}
