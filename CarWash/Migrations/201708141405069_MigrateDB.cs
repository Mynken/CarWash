namespace CarWash.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CarInfoes",
                c => new
                    {
                        CarId = c.Int(nullable: false, identity: true),
                        Model = c.String(),
                        Color = c.String(),
                        ArrivalTime = c.DateTime(nullable: false),
                        PickUpTime = c.DateTime(nullable: false),
                        WashType = c.String(),
                        Cost = c.String(),
                        PaidConfirmed = c.String(),
                        TakeConfirmed = c.String(),
                    })
                .PrimaryKey(t => t.CarId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CarInfoes");
        }
    }
}
