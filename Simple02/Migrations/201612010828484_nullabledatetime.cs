namespace Simple02.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nullabledatetime : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Enquiries", "Expected Rely Date", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Enquiries", "Expected Rely Date", c => c.DateTime(nullable: false));
        }
    }
}
