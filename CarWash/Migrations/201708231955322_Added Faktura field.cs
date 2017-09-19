namespace CarWash.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedFakturafield : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CarInfoes", "Faktura", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CarInfoes", "Faktura");
        }
    }
}
