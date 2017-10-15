namespace Simple02.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DelProviderInAnswer : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Answers", "Provider_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Answers", new[] { "ToEqry_EID" });
            DropIndex("dbo.Answers", new[] { "Provider_Id" });
            AlterColumn("dbo.Answers", "ToEqry_EID", c => c.Guid(nullable: false));
            CreateIndex("dbo.Answers", "ToEqry_EID");
            DropColumn("dbo.Answers", "Provider_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Answers", "Provider_Id", c => c.String(maxLength: 128));
            DropIndex("dbo.Answers", new[] { "ToEqry_EID" });
            AlterColumn("dbo.Answers", "ToEqry_EID", c => c.Guid());
            CreateIndex("dbo.Answers", "Provider_Id");
            CreateIndex("dbo.Answers", "ToEqry_EID");
            AddForeignKey("dbo.Answers", "Provider_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
