namespace CarWash.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedStatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CarInfoes", "Status", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CarInfoes", "Status");
        }
    }
}
