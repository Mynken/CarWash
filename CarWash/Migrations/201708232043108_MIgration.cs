namespace CarWash.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MIgration : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CarInfoes", "PaidConfirmed", c => c.Boolean(nullable: false));
            AlterColumn("dbo.CarInfoes", "TakeConfirmed", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CarInfoes", "TakeConfirmed", c => c.String());
            AlterColumn("dbo.CarInfoes", "PaidConfirmed", c => c.String());
        }
    }
}
