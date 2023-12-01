using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace medical_appointment_scheduling_project.Models
{
    public partial class dbthucuc : DbContext
    {
        public dbthucuc()
            : base("name=dbthucuc")
        {
        }

        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<Appointment> Appointments { get; set; }
        public virtual DbSet<Patient> Patients { get; set; }
        public virtual DbSet<Doctor> Doctor { get; set; }
        public DbSet<ClinicAddress> ClinicAddresses { get; set; }
        public DbSet<Specialization> Specializations { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
