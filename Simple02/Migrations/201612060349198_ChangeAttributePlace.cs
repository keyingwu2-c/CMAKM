namespace Simple02.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeAttributePlace : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Enquiries", "dcsnStatus", c => c.String());
            DropColumn("dbo.Posts", "readyForDiscn");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Posts", "readyForDiscn", c => c.Boolean(nullable: false));
            DropColumn("dbo.Enquiries", "dcsnStatus");
        }
    }
}
