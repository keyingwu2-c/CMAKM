namespace Simple02.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFilesToEqry : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Files",
                c => new
                    {
                        FileId = c.Int(nullable: false, identity: true),
                        FileName = c.String(maxLength: 255),
                        ContentType = c.String(maxLength: 100),
                        Content = c.Binary(),
                        FileType = c.Int(nullable: false),
                        EnquiryId = c.Int(nullable: false),
                        enquiry_EID = c.Guid(),
                    })
                .PrimaryKey(t => t.FileId)
                .ForeignKey("dbo.Enquiries", t => t.enquiry_EID)
                .Index(t => t.enquiry_EID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Files", "enquiry_EID", "dbo.Enquiries");
            DropIndex("dbo.Files", new[] { "enquiry_EID" });
            DropTable("dbo.Files");
        }
    }
}
