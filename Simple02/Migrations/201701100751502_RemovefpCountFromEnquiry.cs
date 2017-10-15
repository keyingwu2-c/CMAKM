namespace Simple02.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovefpCountFromEnquiry : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Enquiries", "fpCount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Enquiries", "fpCount", c => c.Int(nullable: false));
        }
    }
}
