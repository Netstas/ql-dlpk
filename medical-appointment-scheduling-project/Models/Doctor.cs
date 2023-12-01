using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace medical_appointment_scheduling_project.Models
{
    public class Doctor
    {
        public int Id { get; set; }

        [Display(Name = "Tên bác sĩ")]
        [Required(ErrorMessage = "Bạn chưa nhập họ và tên")]
        public string Name { get; set; }

        [Display(Name = "Số điện thoại")]
        [Required(ErrorMessage = "Bạn chưa nhập số điện thoại")]
        public string Phone { get; set; }

        [Display(Name = "Chuyên khoa")]
        [ForeignKey("Specialization")]
        public int? SpecializationId { get; set; }

        [Display(Name = "Cơ sở khám")]
        [ForeignKey("ClinicAddress")]
        public int? ClinicAddressId { get; set; }

        public virtual Specialization Specialization { get; set; }
        public virtual ClinicAddress ClinicAddress { get; set; }
        public virtual ICollection<Patient> Patients { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; }
    }
}
