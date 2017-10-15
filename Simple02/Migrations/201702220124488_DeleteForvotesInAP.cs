namespace Simple02.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteForvotesInAP : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Answers", "forvotes");
            DropColumn("dbo.Posts", "forvotes");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Posts", "forvotes", c => c.Int(nullable: false));
            AddColumn("dbo.Answers", "forvotes", c => c.Int(nullable: false));
        }
    }
}
