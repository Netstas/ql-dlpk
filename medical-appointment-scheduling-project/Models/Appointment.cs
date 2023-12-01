namespace medical_appointment_scheduling_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    public partial class Appointment
    {
        [Key]
        [Column(Order = 0)]
        public int Id { get; set; }

        [Display(Name = "Số thứ tự")]
        [StringLength(20)]
        public string OrdinalCode { get; set; }

        [Display(Name = "Họ và tên")]
        [Required(ErrorMessage = "Bạn chưa nhập họ tên")]
        [StringLength(100)]
        public string FullName { get; set; }

        [Display(Name = "Số điện thoại")]
        [Required(ErrorMessage = "Bạn chưa số điện thoại")]
        [RegularExpression(@"^(\+?\d{1,4})?[ -]?(\d{3}[ -]?\d{3}[ -]?\d{4})$", ErrorMessage = "Số điện thoại không hợp lệ.")]
        [StringLength(20)]
        public string PhoneNumber { get; set; }

        [Display(Name = "Triệu chứng")]
        [Required(ErrorMessage = "Bạn chưa nhập triệu trứng")]
        [StringLength(500)]
        public string Syndrome { get; set; } = null;

        [Display(Name = "Trạng thái")]
        [Required(ErrorMessage = "Bạn chưa chọn trạng thái")]
        public string Status { get; set; } = null;

        [Display(Name = "Ngày đã tạo")]
        [Required(ErrorMessage = "Bạn chưa nhập ngày giờ")]
        public DateTime CreatedAt { get; set; }

        [Display(Name = "Ngày đặt lịch")]
        [Required(ErrorMessage = "Bạn chưa nhập ngày giờ")]
        public DateTime BookingDate { get; set; }

        [Display(Name = "Người quản trị")]
        public int? AdminId { get; set; }
        public int? ClinicAddressId { get; set; }
        public int? SpecializationId { get; set; }
        public int? DoctorId { get; set; }
        public Specialization Specialization { get; set; }
        public ClinicAddress ClinicAddress { get; set; }
        public Doctor Doctor { get; set; }
        public Admin Admin { get; set; }

        public Appointment()
        {
            string uniqueOrdinalCode = GenerateUniqueOrdinalCode();
            OrdinalCode = uniqueOrdinalCode;
            CreatedAt = DateTime.Now;
        }
        private string GenerateUniqueOrdinalCode()
        {
            using (var db = new dbthucuc())
            {
                var existingOrdinals = db.Appointments
                    .Where(a => !string.IsNullOrEmpty(a.OrdinalCode) && a.OrdinalCode.Length <= 4)
                    .Select(a => a.OrdinalCode)
                    .ToList()
                    .Select(code => int.TryParse(code, out int parsedCode) ? parsedCode : 0);

                int lastUsedNumber = existingOrdinals.DefaultIfEmpty(0).Max();

                if (lastUsedNumber >= 9999)
                {
                    throw new Exception("Số lượng mã OrdinalCode đã đạt giới hạn.");
                }

                string newOrdinalCode = (lastUsedNumber + 1).ToString("D4");

                return newOrdinalCode;
            }
        }
    }
}
