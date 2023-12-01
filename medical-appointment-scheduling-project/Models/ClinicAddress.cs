using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace medical_appointment_scheduling_project.Models
{
    public class ClinicAddress
    {
        public int Id { get; set; }

        [Display(Name = "Địa chỉ")]
        [Required(ErrorMessage = "Chưa nhập địa chỉ")]
        public string Address { get; set; }

        [Display(Name = "Số điện thoại")]
        [Required(ErrorMessage = "Chưa nhập số điện thoại")]
        public string Phone { get; set; }

        public virtual ICollection<Specialization> Specializations { get; set; }
        public virtual ICollection<Doctor> Doctors { get; set; }

    }
}