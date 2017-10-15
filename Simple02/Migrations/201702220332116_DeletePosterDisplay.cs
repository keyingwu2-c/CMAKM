namespace Simple02.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeletePosterDisplay : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Posts", "posterDisplay");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Posts", "posterDisplay", c => c.String());
        }
    }
}
