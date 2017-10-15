namespace Simple02.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddContentTypeToFilePathTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FilePaths", "ContentType", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.FilePaths", "ContentType");
        }
    }
}
