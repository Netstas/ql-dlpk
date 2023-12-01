using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace medical_appointment_scheduling_project.Models
{
    public class Specialization
    {
        public int Id { get; set; }

        [Display(Name = "Chuyên khoa")]
        [Required(ErrorMessage = "Bạn chưa nhập tên chuyên khoa")]
        public string Name { get; set; }

        [Display(Name = "Cơ sở khám")]
        [Required(ErrorMessage = "Bạn chưa chọn địa chỉ phòng khám")]
        [ForeignKey("ClinicAddress")]
        public int? ClinicAddressId { get; set; }

        public virtual ClinicAddress ClinicAddress { get; set; }
        public virtual ICollection<Doctor> Doctors { get; set; }
    }
}