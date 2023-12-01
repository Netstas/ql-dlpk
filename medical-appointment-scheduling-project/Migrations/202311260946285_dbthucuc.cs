namespace medical_appointment_scheduling_project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dbthucuc : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false, maxLength: 50),
                        Password = c.String(nullable: false, maxLength: 100),
                        FullName = c.String(nullable: false, maxLength: 100),
                        Email = c.String(),
                        PhoneNumber = c.String(nullable: false, maxLength: 20),
                        Decentralization = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Appointments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrdinalCode = c.String(maxLength: 20),
                        FullName = c.String(nullable: false, maxLength: 100),
                        PhoneNumber = c.String(nullable: false, maxLength: 20),
                        Syndrome = c.String(nullable: false, maxLength: 500),
                        Status = c.String(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        BookingDate = c.DateTime(nullable: false),
                        AdminId = c.Int(),
                        ClinicAddressId = c.Int(),
                        SpecializationId = c.Int(),
                        DoctorId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Admins", t => t.AdminId)
                .ForeignKey("dbo.Doctors", t => t.DoctorId)
                .ForeignKey("dbo.ClinicAddresses", t => t.ClinicAddressId)
                .ForeignKey("dbo.Specializations", t => t.SpecializationId)
                .Index(t => t.AdminId)
                .Index(t => t.ClinicAddressId)
                .Index(t => t.SpecializationId)
                .Index(t => t.DoctorId);
            
            CreateTable(
                "dbo.ClinicAddresses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Doctors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Phone = c.String(nullable: false),
                        SpecializationId = c.Int(),
                        ClinicAddressId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ClinicAddresses", t => t.ClinicAddressId)
                .ForeignKey("dbo.Specializations", t => t.SpecializationId)
                .Index(t => t.SpecializationId)
                .Index(t => t.ClinicAddressId);
            
            CreateTable(
                "dbo.Patients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FullName = c.String(nullable: false),
                        PhoneNumber = c.String(nullable: false),
                        Symptoms = c.String(nullable: false),
                        Status = c.String(nullable: false),
                        Date = c.DateTime(),
                        BookingDate = c.DateTime(nullable: false),
                        AdminId = c.Int(),
                        ClinicAddressId = c.Int(),
                        SpecializationId = c.Int(),
                        DoctorId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Admins", t => t.AdminId)
                .ForeignKey("dbo.ClinicAddresses", t => t.ClinicAddressId)
                .ForeignKey("dbo.Doctors", t => t.DoctorId)
                .ForeignKey("dbo.Specializations", t => t.SpecializationId)
                .Index(t => t.AdminId)
                .Index(t => t.ClinicAddressId)
                .Index(t => t.SpecializationId)
                .Index(t => t.DoctorId);
            
            CreateTable(
                "dbo.Specializations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        ClinicAddressId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ClinicAddresses", t => t.ClinicAddressId, cascadeDelete: true)
                .Index(t => t.ClinicAddressId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Appointments", "SpecializationId", "dbo.Specializations");
            DropForeignKey("dbo.Appointments", "ClinicAddressId", "dbo.ClinicAddresses");
            DropForeignKey("dbo.Doctors", "SpecializationId", "dbo.Specializations");
            DropForeignKey("dbo.Patients", "SpecializationId", "dbo.Specializations");
            DropForeignKey("dbo.Specializations", "ClinicAddressId", "dbo.ClinicAddresses");
            DropForeignKey("dbo.Patients", "DoctorId", "dbo.Doctors");
            DropForeignKey("dbo.Patients", "ClinicAddressId", "dbo.ClinicAddresses");
            DropForeignKey("dbo.Patients", "AdminId", "dbo.Admins");
            DropForeignKey("dbo.Doctors", "ClinicAddressId", "dbo.ClinicAddresses");
            DropForeignKey("dbo.Appointments", "DoctorId", "dbo.Doctors");
            DropForeignKey("dbo.Appointments", "AdminId", "dbo.Admins");
            DropIndex("dbo.Specializations", new[] { "ClinicAddressId" });
            DropIndex("dbo.Patients", new[] { "DoctorId" });
            DropIndex("dbo.Patients", new[] { "SpecializationId" });
            DropIndex("dbo.Patients", new[] { "ClinicAddressId" });
            DropIndex("dbo.Patients", new[] { "AdminId" });
            DropIndex("dbo.Doctors", new[] { "ClinicAddressId" });
            DropIndex("dbo.Doctors", new[] { "SpecializationId" });
            DropIndex("dbo.Appointments", new[] { "DoctorId" });
            DropIndex("dbo.Appointments", new[] { "SpecializationId" });
            DropIndex("dbo.Appointments", new[] { "ClinicAddressId" });
            DropIndex("dbo.Appointments", new[] { "AdminId" });
            DropTable("dbo.Specializations");
            DropTable("dbo.Patients");
            DropTable("dbo.Doctors");
            DropTable("dbo.ClinicAddresses");
            DropTable("dbo.Appointments");
            DropTable("dbo.Admins");
        }
    }
}
