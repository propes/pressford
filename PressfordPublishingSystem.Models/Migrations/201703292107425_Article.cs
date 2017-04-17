namespace PressfordPublishingSystem.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Article : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Articles",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Body = c.String(),
                        AuthorID = c.String(maxLength: 128),
                        DatePublished = c.DateTime(nullable: false),
                        DateModified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.AuthorID)
                .Index(t => t.AuthorID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Articles", "AuthorID", "dbo.AspNetUsers");
            DropIndex("dbo.Articles", new[] { "AuthorID" });
            DropTable("dbo.Articles");
        }
    }
}
