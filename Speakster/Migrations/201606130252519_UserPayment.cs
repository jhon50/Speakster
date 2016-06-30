namespace Speakster.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserPayment : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Teachers", new[] { "User_id" });
            DropPrimaryKey("dbo.Teachers");
            CreateTable(
                "dbo.PayPals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        User_id = c.String(nullable: false, maxLength: 128),
                        Teacher_id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.User_id)
                .ForeignKey("dbo.Teachers", t => t.Teacher_id)
                .Index(t => t.Teacher_id);
            
            AddColumn("dbo.AspNetUsers", "SpeakingLevel_id", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "ListeningLevel_id", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "ProfilePicture", c => c.String());
            AddColumn("dbo.AspNetUsers", "Active", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Teachers", "User_id", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Teachers", "User_id");
            CreateIndex("dbo.Teachers", "User_Id");
            CreateIndex("dbo.AspNetUsers", "SpeakingLevel_id");
            CreateIndex("dbo.AspNetUsers", "ListeningLevel_id");
            AddForeignKey("dbo.AspNetUsers", "ListeningLevel_id", "dbo.ListeningLevels", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AspNetUsers", "SpeakingLevel_id", "dbo.SpeakingLevels", "Id", cascadeDelete: true);
            DropColumn("dbo.Teachers", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Teachers", "Id", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.Students", "Teacher_id", "dbo.Teachers");
            DropForeignKey("dbo.AspNetUsers", "SpeakingLevel_id", "dbo.SpeakingLevels");
            DropForeignKey("dbo.AspNetUsers", "ListeningLevel_id", "dbo.ListeningLevels");
            DropIndex("dbo.AspNetUsers", new[] { "ListeningLevel_id" });
            DropIndex("dbo.AspNetUsers", new[] { "SpeakingLevel_id" });
            DropIndex("dbo.Teachers", new[] { "User_Id" });
            DropIndex("dbo.Students", new[] { "Teacher_id" });
            DropPrimaryKey("dbo.Teachers");
            AlterColumn("dbo.Teachers", "User_id", c => c.String(maxLength: 128));
            DropColumn("dbo.AspNetUsers", "Active");
            DropColumn("dbo.AspNetUsers", "ProfilePicture");
            DropColumn("dbo.AspNetUsers", "ListeningLevel_id");
            DropColumn("dbo.AspNetUsers", "SpeakingLevel_id");
            DropTable("dbo.Students");
            DropTable("dbo.PayPals");
            AddPrimaryKey("dbo.Teachers", "Id");
            CreateIndex("dbo.Teachers", "User_id");
        }
    }
}
