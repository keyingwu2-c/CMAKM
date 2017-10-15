namespace Simple02.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddfpCountToEnquiry : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Enquiries", "fpCount", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Enquiries", "fpCount");
        }
    }
}
