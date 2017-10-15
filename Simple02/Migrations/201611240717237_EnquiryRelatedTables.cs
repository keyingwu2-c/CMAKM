namespace Simple02.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EnquiryRelatedTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        customer_code = c.String(nullable: false, maxLength: 128),
                        customer_name = c.String(),
                    })
                .PrimaryKey(t => t.customer_code);
            
            CreateTable(
                "dbo.Enquiries",
                c => new
                    {
                        Enquiry_id = c.Int(nullable: false),
                        original_question = c.String(),
                        consolidation = c.String(),
                        customer_code = c.String(),
                        ExpectedRelyDate = c.DateTime(name: "Expected Rely Date"),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        Noter_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Enquiry_id)
                .ForeignKey("dbo.AspNetUsers", t => t.Noter_Id)
                .Index(t => t.Noter_Id);
            
            AddColumn("dbo.Posts", "ToEqry_EID", c => c.Int());
            CreateIndex("dbo.Posts", "ToEqry_EID");
            AddForeignKey("dbo.Posts", "ToEqry_EID", "dbo.Enquiries", "Enquiry_id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Posts", "ToEqry_EID", "dbo.Enquiries");
            DropForeignKey("dbo.Enquiries", "Noter_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Posts", new[] { "ToEqry_EID" });
            DropIndex("dbo.Enquiries", new[] { "Noter_Id" });
            DropColumn("dbo.Posts", "ToEqry_EID");
            DropTable("dbo.Enquiries");
            DropTable("dbo.Customers");
        }
    }
}
