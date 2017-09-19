namespace CarWash.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CarInfoes", "Cost", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CarInfoes", "Cost", c => c.String(nullable: false));
        }
    }
}
