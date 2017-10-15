namespace Simple02.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deleteEnqPos : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Enquiries", "fromCstr_CID", "dbo.Customers");
            DropForeignKey("dbo.Enquiries", "Noter_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Posts", "Poster_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Posts", "ToEqry_EID", "dbo.Enquiries");
            DropIndex("dbo.Enquiries", new[] { "fromCstr_CID" });
            DropIndex("dbo.Enquiries", new[] { "Noter_Id" });
            DropIndex("dbo.Posts", new[] { "Poster_Id" });
            DropIndex("dbo.Posts", new[] { "ToEqry_EID" });
            DropTable("dbo.Enquiries");
            DropTable("dbo.Posts");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        PID = c.Int(nullable: false, identity: true),
                        Poster_Id = c.String(maxLength: 128),
                        ToEqry_EID = c.Guid(),
                    })
                .PrimaryKey(t => t.PID);
            
            CreateTable(
                "dbo.Enquiries",
                c => new
                    {
                        Enquiry_id = c.Guid(nullable: false, identity: true),
                        lastUpated = c.DateTime(nullable: false),
                        original_question = c.String(),
                        consolidation = c.String(),
                        ExpectedRelyDate = c.DateTime(name: "Expected Rely Date"),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        fromCstr_CID = c.Guid(),
                        Noter_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Enquiry_id);
            
            CreateIndex("dbo.Posts", "ToEqry_EID");
            CreateIndex("dbo.Posts", "Poster_Id");
            CreateIndex("dbo.Enquiries", "Noter_Id");
            CreateIndex("dbo.Enquiries", "fromCstr_CID");
            AddForeignKey("dbo.Posts", "ToEqry_EID", "dbo.Enquiries", "Enquiry_id");
            AddForeignKey("dbo.Posts", "Poster_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Enquiries", "Noter_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Enquiries", "fromCstr_CID", "dbo.Customers", "customer_code");
        }
    }
}
