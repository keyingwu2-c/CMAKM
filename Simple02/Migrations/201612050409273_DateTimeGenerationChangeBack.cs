namespace Simple02.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DateTimeGenerationChangeBack : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Enquiries", "lastUpated", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Enquiries", "lastUpated", c => c.DateTime(nullable: false));
        }
    }
}
