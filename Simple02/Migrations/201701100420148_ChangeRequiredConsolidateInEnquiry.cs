namespace Simple02.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeRequiredConsolidateInEnquiry : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Enquiries", "consolidation", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Enquiries", "consolidation", c => c.String(nullable: false));
        }
    }
}
