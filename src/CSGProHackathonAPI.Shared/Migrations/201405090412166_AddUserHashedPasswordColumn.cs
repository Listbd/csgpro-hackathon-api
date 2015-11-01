namespace CSGProHackathonAPI.Shared.Migrations
{
    using CSGProHackathonAPI.Shared.Infrastructure;
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserHashedPasswordColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "HashedPassword", c => c.String(nullable: true, maxLength: 255));

            var hashedPassword = Security.GetSwcSH1("gamehead");
            Sql(string.Format("update [User] set HashedPassword = '{0}' where UserId = {1}", hashedPassword, 1));

            AlterColumn("dbo.User", "HashedPassword", c => c.String(nullable: false, maxLength: 255));
        }
        
        public override void Down()
        {
            DropColumn("dbo.User", "HashedPassword");
        }
    }
}
