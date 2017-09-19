namespace CarWash.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Payment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CarInfoes", "Payment", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CarInfoes", "Payment");
        }
    }
}
