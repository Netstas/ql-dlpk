using medical_appointment_scheduling_project.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace medical_appointment_scheduling_project.ViewModel
{
    public class AdviseViewModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string ClinicAddress { get; set; } 
        public string Symptoms { get; set; } 
        public string Specialization { get; set; } 
        public string Status { get; set; } 
        public DateTime? Date { get; set; }
        public DateTime? BookingDate { get; set; }
        public int? AdminId { get; set; }
        public string DoctorName { get; set; }
    }
}