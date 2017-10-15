namespace Simple02.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddExptsCalledToEnquiry : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ExpertEnquiries",
                c => new
                    {
                        Expert_Id = c.String(nullable: false, maxLength: 128),
                        Enquiry_EID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Expert_Id, t.Enquiry_EID })
                .ForeignKey("dbo.AspNetUsers", t => t.Expert_Id, cascadeDelete: true)
                .ForeignKey("dbo.Enquiries", t => t.Enquiry_EID, cascadeDelete: true)
                .Index(t => t.Expert_Id)
                .Index(t => t.Enquiry_EID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ExpertEnquiries", "Enquiry_EID", "dbo.Enquiries");
            DropForeignKey("dbo.ExpertEnquiries", "Expert_Id", "dbo.AspNetUsers");
            DropIndex("dbo.ExpertEnquiries", new[] { "Enquiry_EID" });
            DropIndex("dbo.ExpertEnquiries", new[] { "Expert_Id" });
            DropTable("dbo.ExpertEnquiries");
        }
    }
}
