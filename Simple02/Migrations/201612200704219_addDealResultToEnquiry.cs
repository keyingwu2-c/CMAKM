namespace Simple02.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addDealResultToEnquiry : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Enquiries", "BssResult", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Enquiries", "BssResult");
        }
    }
}
