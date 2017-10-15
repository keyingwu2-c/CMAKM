namespace Simple02.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteExpertClass : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Enquiries", "Expert_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Enquiries", new[] { "Expert_Id" });
            DropColumn("dbo.Enquiries", "Expert_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Enquiries", "Expert_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Enquiries", "Expert_Id");
            AddForeignKey("dbo.Enquiries", "Expert_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
