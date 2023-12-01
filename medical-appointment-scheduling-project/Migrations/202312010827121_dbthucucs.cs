namespace medical_appointment_scheduling_project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dbthucucs : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ClinicAddresses", "Phone", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ClinicAddresses", "Phone");
        }
    }
}
