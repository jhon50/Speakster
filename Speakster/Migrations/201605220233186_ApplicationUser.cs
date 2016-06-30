namespace Speakster.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ApplicationUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Language_id", c => c.Int(nullable: true));
            CreateIndex("dbo.AspNetUsers", "Language_id");
            AddForeignKey("dbo.AspNetUsers", "Language_id", "dbo.Languages", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "Language_id", "dbo.Languages");
            DropIndex("dbo.AspNetUsers", new[] { "Language_id" });
            DropColumn("dbo.AspNetUsers", "Language_id");
        }
    }
}
