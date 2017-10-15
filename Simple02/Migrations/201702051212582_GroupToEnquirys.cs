namespace Simple02.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GroupToEnquirys : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Enquiries", "DcnGroup_GID", c => c.Int());
            CreateIndex("dbo.Enquiries", "DcnGroup_GID");
            AddForeignKey("dbo.Enquiries", "DcnGroup_GID", "dbo.Groups", "GroupID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Enquiries", "DcnGroup_GID", "dbo.Groups");
            DropIndex("dbo.Enquiries", new[] { "DcnGroup_GID" });
            DropColumn("dbo.Enquiries", "DcnGroup_GID");
        }
    }
}
