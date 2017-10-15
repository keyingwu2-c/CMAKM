namespace Simple02.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class KeepConnectionByPosts : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.EnquiryExperts", "Enquiry_EID", "dbo.Enquiries");
            DropForeignKey("dbo.EnquiryExperts", "Expert_Id", "dbo.AspNetUsers");
            DropIndex("dbo.EnquiryExperts", new[] { "Enquiry_EID" });
            DropIndex("dbo.EnquiryExperts", new[] { "Expert_Id" });
            AddColumn("dbo.Posts", "content", c => c.String());
            AddColumn("dbo.Posts", "IsAnonymous", c => c.Boolean(nullable: false));
            DropTable("dbo.EnquiryExperts");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.EnquiryExperts",
                c => new
                    {
                        Enquiry_EID = c.Guid(nullable: false),
                        Expert_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Enquiry_EID, t.Expert_Id });
            
            DropColumn("dbo.Posts", "IsAnonymous");
            DropColumn("dbo.Posts", "content");
            CreateIndex("dbo.EnquiryExperts", "Expert_Id");
            CreateIndex("dbo.EnquiryExperts", "Enquiry_EID");
            AddForeignKey("dbo.EnquiryExperts", "Expert_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.EnquiryExperts", "Enquiry_EID", "dbo.Enquiries", "Enquiry_id", cascadeDelete: true);
        }
    }
}
