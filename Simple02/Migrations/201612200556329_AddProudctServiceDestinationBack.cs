namespace Simple02.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProudctServiceDestinationBack : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Enquiries", "Product", c => c.String());
            AddColumn("dbo.Enquiries", "Service", c => c.String());
            AddColumn("dbo.Enquiries", "Destination", c => c.String());
            AddColumn("dbo.Enquiries", "Regulation", c => c.String());
            AddColumn("dbo.Topics", "TName", c => c.String());
            DropColumn("dbo.Enquiries", "RegulationType");
            DropColumn("dbo.Enquiries", "RegulationName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Enquiries", "RegulationName", c => c.String());
            AddColumn("dbo.Enquiries", "RegulationType", c => c.String());
            DropColumn("dbo.Topics", "TName");
            DropColumn("dbo.Enquiries", "Regulation");
            DropColumn("dbo.Enquiries", "Destination");
            DropColumn("dbo.Enquiries", "Service");
            DropColumn("dbo.Enquiries", "Product");
        }
    }
}
