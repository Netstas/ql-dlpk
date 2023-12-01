namespace medical_appointment_scheduling_project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dbthucus : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ClinicAddresses", "Phone", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ClinicAddresses", "Phone", c => c.Double(nullable: false));
        }
    }
}
