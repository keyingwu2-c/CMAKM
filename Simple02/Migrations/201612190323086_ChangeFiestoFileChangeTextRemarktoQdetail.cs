namespace Simple02.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeFiestoFileChangeTextRemarktoQdetail : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Enquiries", "Qdetail", c => c.String());
            AddColumn("dbo.Enquiries", "File", c => c.Binary());
            DropColumn("dbo.Enquiries", "TextRemark");
            DropColumn("dbo.Enquiries", "Files");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Enquiries", "Files", c => c.Binary());
            AddColumn("dbo.Enquiries", "TextRemark", c => c.String());
            DropColumn("dbo.Enquiries", "File");
            DropColumn("dbo.Enquiries", "Qdetail");
        }
    }
}
