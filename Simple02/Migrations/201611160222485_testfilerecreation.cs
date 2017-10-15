namespace Simple02.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class testfilerecreation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        PID = c.Int(nullable: false, identity: true),
                        Poster_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.PID)
                .ForeignKey("dbo.AspNetUsers", t => t.Poster_Id)
                .Index(t => t.Poster_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Posts", "Poster_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Posts", new[] { "Poster_Id" });
            DropTable("dbo.Posts");
        }
    }
}
