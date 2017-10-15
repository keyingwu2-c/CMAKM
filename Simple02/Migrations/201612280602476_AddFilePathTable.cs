namespace Simple02.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFilePathTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FilePaths",
                c => new
                    {
                        FilePathId = c.Int(nullable: false, identity: true),
                        FileName = c.String(maxLength: 255),
                        FileType = c.Int(nullable: false),
                        enquiry_EID = c.Guid(),
                    })
                .PrimaryKey(t => t.FilePathId)
                .ForeignKey("dbo.Enquiries", t => t.enquiry_EID)
                .Index(t => t.enquiry_EID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FilePaths", "enquiry_EID", "dbo.Enquiries");
            DropIndex("dbo.FilePaths", new[] { "enquiry_EID" });
            DropTable("dbo.FilePaths");
        }
    }
}
