namespace Simple02.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EnquiryAssignment : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EnquiryExperts",
                c => new
                    {
                        Enquiry_EID = c.Guid(nullable: false),
                        Expert_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Enquiry_EID, t.Expert_Id })
                .ForeignKey("dbo.Enquiries", t => t.Enquiry_EID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.Expert_Id, cascadeDelete: true)
                .Index(t => t.Enquiry_EID)
                .Index(t => t.Expert_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EnquiryExperts", "Expert_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.EnquiryExperts", "Enquiry_EID", "dbo.Enquiries");
            DropIndex("dbo.EnquiryExperts", new[] { "Expert_Id" });
            DropIndex("dbo.EnquiryExperts", new[] { "Enquiry_EID" });
            DropTable("dbo.EnquiryExperts");
        }
    }
}
