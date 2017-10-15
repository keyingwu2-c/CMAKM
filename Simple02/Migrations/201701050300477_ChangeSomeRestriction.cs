namespace Simple02.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteFields : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Enquiries", "Product", c => c.String(nullable: false));
            AlterColumn("dbo.Enquiries", "consolidation", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Enquiries", "consolidation", c => c.String());
            AlterColumn("dbo.Enquiries", "Product", c => c.String());
        }
    }
}
