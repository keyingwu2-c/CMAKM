namespace Simple02.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeFileIntoFileCountInEqry : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Enquiries", "FileCount", c => c.Int(nullable: false));
            DropColumn("dbo.Enquiries", "File");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Enquiries", "File", c => c.Binary());
            DropColumn("dbo.Enquiries", "FileCount");
        }
    }
}
