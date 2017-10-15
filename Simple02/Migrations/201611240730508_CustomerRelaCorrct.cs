namespace Simple02.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CustomerRelaCorrct : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Enquiries", "fromCstr_CID", c => c.String(maxLength: 128));
            CreateIndex("dbo.Enquiries", "fromCstr_CID");
            AddForeignKey("dbo.Enquiries", "fromCstr_CID", "dbo.Customers", "customer_code");
            DropColumn("dbo.Enquiries", "customer_code");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Enquiries", "customer_code", c => c.String());
            DropForeignKey("dbo.Enquiries", "fromCstr_CID", "dbo.Customers");
            DropIndex("dbo.Enquiries", new[] { "fromCstr_CID" });
            DropColumn("dbo.Enquiries", "fromCstr_CID");
        }
    }
}
