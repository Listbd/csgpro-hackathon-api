namespace CSGProHackathonAPI.Shared.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProjectRole",
                c => new
                    {
                        ProjectRoleId = c.Int(nullable: false, identity: true),
                        ProjectId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 100),
                        ExternalSystemKey = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ProjectRoleId)
                .ForeignKey("dbo.Project", t => t.ProjectId)
                .Index(t => t.ProjectId);
            
            CreateTable(
                "dbo.Project",
                c => new
                    {
                        ProjectId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 100),
                        ExternalSystemKey = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ProjectId)
                .ForeignKey("dbo.User", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.ProjectTask",
                c => new
                    {
                        ProjectTaskId = c.Int(nullable: false, identity: true),
                        ProjectId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 100),
                        Billable = c.Boolean(nullable: false),
                        RequireComment = c.Boolean(nullable: false),
                        ExternalSystemKey = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ProjectTaskId)
                .ForeignKey("dbo.Project", t => t.ProjectId)
                .Index(t => t.ProjectId);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false, maxLength: 50),
                        Name = c.String(nullable: false, maxLength: 100),
                        Email = c.String(nullable: false, maxLength: 255),
                        TimeZoneId = c.String(nullable: false, maxLength: 100),
                        UseStopwatchApproachToTimeEntry = c.Boolean(nullable: false),
                        ExternalSystemKey = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.TimeEntry",
                c => new
                    {
                        TimeEntryId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ProjectRoleId = c.Int(nullable: false),
                        ProjectTaskId = c.Int(nullable: false),
                        Billable = c.Boolean(nullable: false),
                        TimeInUtc = c.DateTime(nullable: false),
                        TimeOutUtc = c.DateTime(),
                        Hours = c.Decimal(precision: 4, scale: 2),
                        Comment = c.String(),
                    })
                .PrimaryKey(t => t.TimeEntryId)
                .ForeignKey("dbo.ProjectRole", t => t.ProjectRoleId)
                .ForeignKey("dbo.ProjectTask", t => t.ProjectTaskId)
                .ForeignKey("dbo.User", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.ProjectRoleId)
                .Index(t => t.ProjectTaskId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TimeEntry", "UserId", "dbo.User");
            DropForeignKey("dbo.TimeEntry", "ProjectTaskId", "dbo.ProjectTask");
            DropForeignKey("dbo.TimeEntry", "ProjectRoleId", "dbo.ProjectRole");
            DropForeignKey("dbo.ProjectRole", "ProjectId", "dbo.Project");
            DropForeignKey("dbo.Project", "UserId", "dbo.User");
            DropForeignKey("dbo.ProjectTask", "ProjectId", "dbo.Project");
            DropIndex("dbo.TimeEntry", new[] { "ProjectTaskId" });
            DropIndex("dbo.TimeEntry", new[] { "ProjectRoleId" });
            DropIndex("dbo.TimeEntry", new[] { "UserId" });
            DropIndex("dbo.ProjectTask", new[] { "ProjectId" });
            DropIndex("dbo.Project", new[] { "UserId" });
            DropIndex("dbo.ProjectRole", new[] { "ProjectId" });
            DropTable("dbo.TimeEntry");
            DropTable("dbo.User");
            DropTable("dbo.ProjectTask");
            DropTable("dbo.Project");
            DropTable("dbo.ProjectRole");
        }
    }
}
