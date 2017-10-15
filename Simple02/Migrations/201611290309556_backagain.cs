namespace Simple02.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class backagain : DbMigration
    {
        public override void Up()
        {
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
                .PrimaryKey(t => t.Enquiry_id)
                .ForeignKey("dbo.Customers", t => t.fromCstr_CID)
                .ForeignKey("dbo.AspNetUsers", t => t.Noter_Id)
                .Index(t => t.fromCstr_CID)
                .Index(t => t.Noter_Id);
            
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        PID = c.Int(nullable: false, identity: true),
                        Poster_Id = c.String(maxLength: 128),
                        ToEqry_EID = c.Guid(),
                    })
                .PrimaryKey(t => t.PID)
                .ForeignKey("dbo.AspNetUsers", t => t.Poster_Id)
                .ForeignKey("dbo.Enquiries", t => t.ToEqry_EID)
                .Index(t => t.Poster_Id)
                .Index(t => t.ToEqry_EID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Posts", "ToEqry_EID", "dbo.Enquiries");
            DropForeignKey("dbo.Posts", "Poster_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Enquiries", "Noter_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Enquiries", "fromCstr_CID", "dbo.Customers");
            DropIndex("dbo.Posts", new[] { "ToEqry_EID" });
            DropIndex("dbo.Posts", new[] { "Poster_Id" });
            DropIndex("dbo.Enquiries", new[] { "Noter_Id" });
            DropIndex("dbo.Enquiries", new[] { "fromCstr_CID" });
            DropTable("dbo.Posts");
            DropTable("dbo.Enquiries");
        }
    }
}
