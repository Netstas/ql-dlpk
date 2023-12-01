using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Helpers;

namespace medical_appointment_scheduling_project.Models
{
    public class Patient
    {
        public int Id { get; set; }
        [Display(Name = "Họ và tên")]
        [Required(ErrorMessage = "Bạn chưa nhập họ và tên")]
        public string FullName { get; set; }
        [Display(Name = "Số điện thoại")]
        [Required(ErrorMessage = "Bạn chưa nhập số điện thoại")]
        public string PhoneNumber { get; set; }
        
        [Display(Name = "Triệu chứng")]
        [Required(ErrorMessage = "Bạn chưa nhập triệu trứng")]
        public string Symptoms { get; set; } = null;

        [Display(Name = "Trạng thái")]
        [Required(ErrorMessage = "Bạn chưa nhập triệu trứng")]
        public string Status { get; set; } = null;
        [Display(Name = "Ngày được tạo")]
        public DateTime? Date { get; set; }

        [Display(Name = "Ngày đặt lịch hẹn")]
        [Required(ErrorMessage = "Bạn chưa nhập ngày giờ")]
        public DateTime BookingDate { get; set; }

        public int? AdminId { get; set; }
        public int? ClinicAddressId { get; set; }
        public int? SpecializationId { get; set; }
        public int? DoctorId { get; set; }
        public Specialization Specialization { get; set; }
        public ClinicAddress ClinicAddress { get; set; }
        public Doctor Doctor { get; set; }
        public Admin Admin { get; set; }

        public Patient()
        {
            Date = DateTime.Now;

        }
    }
}