namespace Simple02.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changesunremember : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Enquiries", "lastUpated", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Enquiries", "lastUpated");
        }
    }
}
