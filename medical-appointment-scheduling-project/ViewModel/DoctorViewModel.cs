using medical_appointment_scheduling_project.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace medical_appointment_scheduling_project.ViewModel
{
    public class DoctorViewModel
    {
        public int Id { get; set; }
        public string OrdinalCode { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Specialization { get; set; } = null;
        public string Syndrome { get; set; } = null;
        public string DoctorName { get; set; }
        public string ClinicAddress { get; set; } = null;
        public string Status { get; set; } = null;
        public DateTime CreatedAt { get; set; }
        public DateTime BookingDate { get; set; }
        public int? AdminId { get; set; }
    }
}