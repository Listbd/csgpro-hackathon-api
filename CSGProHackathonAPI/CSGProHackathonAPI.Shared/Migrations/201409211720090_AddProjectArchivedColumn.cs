namespace CSGProHackathonAPI.Shared.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProjectArchivedColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Project", "Archived", c => c.Boolean(nullable: false, defaultValue: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Project", "Archived");
        }
    }
}
