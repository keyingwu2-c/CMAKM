namespace Simple02.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeTitleConstraintToEnquiry : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Enquiries", "qtn_tile", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Enquiries", "qtn_tile", c => c.String());
        }
    }
}
