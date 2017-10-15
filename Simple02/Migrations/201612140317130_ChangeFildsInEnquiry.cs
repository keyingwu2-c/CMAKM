namespace Simple02.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeFildsInEnquiry : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Enquiries", "qtn_tile", c => c.String());
            DropColumn("dbo.Enquiries", "original_question");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Enquiries", "original_question", c => c.String());
            DropColumn("dbo.Enquiries", "qtn_tile");
        }
    }
}
