namespace medical_appointment_scheduling_project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dbthucucc : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ClinicAddresses", "Phone", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ClinicAddresses", "Phone", c => c.Int(nullable: false));
        }
    }
}
