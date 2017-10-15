namespace Simple02.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class votescounted : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "forvotes", c => c.Int(nullable: false));
            AddColumn("dbo.Posts", "againstvotes", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "againstvotes");
            DropColumn("dbo.Posts", "forvotes");
        }
    }
}
