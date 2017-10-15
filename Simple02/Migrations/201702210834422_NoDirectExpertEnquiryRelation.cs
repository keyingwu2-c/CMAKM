namespace Simple02.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NoDirectExpertEnquiryRelation : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ExpertEnquiries", "Expert_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.ExpertEnquiries", "Enquiry_EID", "dbo.Enquiries");
            DropIndex("dbo.ExpertEnquiries", new[] { "Expert_Id" });
            DropIndex("dbo.ExpertEnquiries", new[] { "Enquiry_EID" });
            AddColumn("dbo.Answers", "ApplicationUser_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Enquiries", "Expert_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Answers", "ApplicationUser_Id");
            CreateIndex("dbo.Enquiries", "Expert_Id");
            AddForeignKey("dbo.Answers", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Enquiries", "Expert_Id", "dbo.AspNetUsers", "Id");
            DropTable("dbo.ExpertEnquiries");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ExpertEnquiries",
                c => new
                    {
                        Expert_Id = c.String(nullable: false, maxLength: 128),
                        Enquiry_EID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Expert_Id, t.Enquiry_EID });
            
            DropForeignKey("dbo.Enquiries", "Expert_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Answers", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Enquiries", new[] { "Expert_Id" });
            DropIndex("dbo.Answers", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.Enquiries", "Expert_Id");
            DropColumn("dbo.Answers", "ApplicationUser_Id");
            CreateIndex("dbo.ExpertEnquiries", "Enquiry_EID");
            CreateIndex("dbo.ExpertEnquiries", "Expert_Id");
            AddForeignKey("dbo.ExpertEnquiries", "Enquiry_EID", "dbo.Enquiries", "Enquiry_id", cascadeDelete: true);
            AddForeignKey("dbo.ExpertEnquiries", "Expert_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
