namespace Simple02.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAttributestoEqry : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Enquiries", "Product", c => c.String());
            AddColumn("dbo.Enquiries", "Service", c => c.String());
            AddColumn("dbo.Enquiries", "Destination", c => c.String());
            DropColumn("dbo.Enquiries", "Discriminator");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Enquiries", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.Enquiries", "Destination");
            DropColumn("dbo.Enquiries", "Service");
            DropColumn("dbo.Enquiries", "Product");
        }
    }
}
