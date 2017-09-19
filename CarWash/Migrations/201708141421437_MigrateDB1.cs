namespace CarWash.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CarInfoes", "PickUpTime", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CarInfoes", "PickUpTime", c => c.DateTime(nullable: false));
        }
    }
}
