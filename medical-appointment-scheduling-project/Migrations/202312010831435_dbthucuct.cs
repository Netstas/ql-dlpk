namespace medical_appointment_scheduling_project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dbthucuct : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ClinicAddresses", "Address", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ClinicAddresses", "Address", c => c.String());
        }
    }
}
