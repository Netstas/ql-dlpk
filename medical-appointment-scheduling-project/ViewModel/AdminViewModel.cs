using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace medical_appointment_scheduling_project.ViewModel
{
    public class AdminViewModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; } = null;
        public string Email { get; set; }
        public string PhoneNumber { get; set; } = null;
        public string Decentralization { get; set; } = null;
    }
}