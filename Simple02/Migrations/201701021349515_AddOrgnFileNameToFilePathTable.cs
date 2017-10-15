namespace Simple02.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOrgnFileNameToFilePathTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FilePaths", "OrgnFileName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.FilePaths", "OrgnFileName");
        }
    }
}
