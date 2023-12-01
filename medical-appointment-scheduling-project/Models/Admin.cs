namespace medical_appointment_scheduling_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Admin
    {
        public int Id { get; set; }

        [Display(Name = "Tên đăng nhập")]
        [Required(ErrorMessage = "Bạn chưa nhập tên tài khoản")]
        [StringLength(50)]
        public string Username { get; set; }
        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "Bạn chưa nhập mật khẩu")]
        [StringLength(100)]
        public string Password { get; set; }

        [Display(Name = "Họ và Tên")]
        [Required(ErrorMessage = "Bạn chưa nhập họ và tên")]
        [StringLength(100)]
        public string FullName { get; set; } = null;

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Số điện thoại")]
        [Required(ErrorMessage = "Bạn chưa nhập số điện thoại")]
        [StringLength(20)]
        public string PhoneNumber { get; set; } = null;

        [Display(Name = "Phân quyền")]
        [Required(ErrorMessage = "Bạn chưa chọn phân quyền")]
        public string Decentralization { get; set; } = null;
        public virtual ICollection<Patient> Patients { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; }
    }
}
