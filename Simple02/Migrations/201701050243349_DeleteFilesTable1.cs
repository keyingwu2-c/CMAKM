namespace Simple02.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteFilesTable1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Files", "enquiry_EID", "dbo.Enquiries");
            DropIndex("dbo.Files", new[] { "enquiry_EID" });
            DropTable("dbo.Files");
        }
        
        public override void Down()
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
                .PrimaryKey(t => t.FileId);
            
            CreateIndex("dbo.Files", "enquiry_EID");
            AddForeignKey("dbo.Files", "enquiry_EID", "dbo.Enquiries", "Enquiry_id");
        }
    }
}
