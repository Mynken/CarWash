namespace CarWash.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedFaktura : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CarInfoes", "Faktura", c => c.Boolean(nullable: true));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CarInfoes", "Faktura", c => c.String());
        }
    }
}
