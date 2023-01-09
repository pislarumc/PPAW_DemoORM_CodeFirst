namespace NivelAccesDate_ORM_CodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.ModelConfiguration.Configuration;

    public partial class DataBaseCodeFirstV_13 : DbMigration
    {
        public override void Up()
        {
            Sql("Insert into [LaboratorCodeFirst].[dbo].[Users] Values('utilizator','utilizator','utilizator','utilizator','utilizator',1)"); 
        }
        
        public override void Down()
        {
        }
    }
}
