namespace Simple02.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPageView : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Enquiries", "pageview", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Enquiries", "pageview");
        }
    }
}
