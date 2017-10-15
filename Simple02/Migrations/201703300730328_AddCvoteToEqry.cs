namespace Simple02.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCvoteToEqry : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Enquiries", "cvoting_VID", c => c.Int());
            CreateIndex("dbo.Enquiries", "cvoting_VID");
            AddForeignKey("dbo.Enquiries", "cvoting_VID", "dbo.Votes", "VID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Enquiries", "cvoting_VID", "dbo.Votes");
            DropIndex("dbo.Enquiries", new[] { "cvoting_VID" });
            DropColumn("dbo.Enquiries", "cvoting_VID");
        }
    }
}
