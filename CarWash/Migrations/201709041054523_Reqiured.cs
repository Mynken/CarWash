namespace CarWash.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Reqiured : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CarInfoes", "Model", c => c.String(nullable: false));
            AlterColumn("dbo.CarInfoes", "Cost", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CarInfoes", "Cost", c => c.String());
            AlterColumn("dbo.CarInfoes", "Model", c => c.String());
        }
    }
}
