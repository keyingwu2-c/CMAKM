namespace Simple02.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class subclasses : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Enquiries", "KeyElementsSpecified", c => c.String());
            AddColumn("dbo.Enquiries", "TextRemark", c => c.String());
            AddColumn("dbo.Enquiries", "Files", c => c.Binary());
            AddColumn("dbo.Enquiries", "TypeSpecified", c => c.String());
            AddColumn("dbo.Enquiries", "RegulationType", c => c.String());
            AddColumn("dbo.Enquiries", "RegulationName", c => c.String());
            
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Enquiries", "Expected Rely Date", c => c.DateTime());
            DropColumn("dbo.Enquiries", "RegulationName");
            DropColumn("dbo.Enquiries", "RegulationType");
            DropColumn("dbo.Enquiries", "TypeSpecified");
            DropColumn("dbo.Enquiries", "Files");
            DropColumn("dbo.Enquiries", "TextRemark");
            DropColumn("dbo.Enquiries", "KeyElementsSpecified");
        }
    }
}
