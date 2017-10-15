namespace Simple02.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCmpltdDtToEnqry : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Enquiries", "CompletedDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Enquiries", "CompletedDate");
        }
    }
}
