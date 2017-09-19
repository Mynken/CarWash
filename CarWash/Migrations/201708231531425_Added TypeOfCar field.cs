namespace CarWash.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedTypeOfCarfield : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CarInfoes", "TypeOfCar", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CarInfoes", "TypeOfCar");
        }
    }
}
