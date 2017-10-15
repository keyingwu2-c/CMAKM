namespace Simple02.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTopicTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Topics",
                c => new
                    {
                        TID = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.TID);
            
            CreateTable(
                "dbo.TopicEnquiries",
                c => new
                    {
                        Topic_TID = c.Int(nullable: false),
                        Enquiry_EID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Topic_TID, t.Enquiry_EID })
                .ForeignKey("dbo.Topics", t => t.Topic_TID, cascadeDelete: true)
                .ForeignKey("dbo.Enquiries", t => t.Enquiry_EID, cascadeDelete: true)
                .Index(t => t.Topic_TID)
                .Index(t => t.Enquiry_EID);
            
            DropColumn("dbo.Enquiries", "Product");
            DropColumn("dbo.Enquiries", "Service");
            DropColumn("dbo.Enquiries", "Destination");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Enquiries", "Destination", c => c.String());
            AddColumn("dbo.Enquiries", "Service", c => c.String());
            AddColumn("dbo.Enquiries", "Product", c => c.String());
            DropForeignKey("dbo.TopicEnquiries", "Enquiry_EID", "dbo.Enquiries");
            DropForeignKey("dbo.TopicEnquiries", "Topic_TID", "dbo.Topics");
            DropIndex("dbo.TopicEnquiries", new[] { "Enquiry_EID" });
            DropIndex("dbo.TopicEnquiries", new[] { "Topic_TID" });
            DropTable("dbo.TopicEnquiries");
            DropTable("dbo.Topics");
        }
    }
}
