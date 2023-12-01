using medical_appointment_scheduling_project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Web.Helpers;
using System.IO;
using PagedList;
using System.Web.UI;
using System.Data.Entity;
using System.Data.Entity.Validation;
using medical_appointment_scheduling_project.ViewModel;

namespace medical_appointment_scheduling_project.Controllers
{
    public class InfoController : Controller
    {
        private dbthucuc db = new dbthucuc();

        [Route("~/info")]
        [Route("~/info/index")]
        public ActionResult Index()
        {
            var admins = db.Admins.ToList();

            var appointments = db.Appointments
                 .Include(a => a.Doctor)
                 .OrderBy(a => a.Id)
                 .Select(a => new DoctorViewModel
                 {
                     Id = a.Id,
                     AdminId = a.AdminId,
                     OrdinalCode = a.OrdinalCode,
                     FullName = a.FullName,
                     PhoneNumber = a.PhoneNumber,
                     Status = a.Status,
                     DoctorName = a.Doctor.Name
                 });

            var patients = db.Patients
               .Include(a => a.Doctor)
               .OrderBy(a => a.Id)
               .Select(a => new AdviseViewModel
               {
                   Id = a.Id,
                   AdminId = a.AdminId,
                   FullName = a.FullName,
                   PhoneNumber = a.PhoneNumber,
                   ClinicAddress = a.ClinicAddress.Address,
                   Specialization = a.Specialization.Name,
                   DoctorName = a.Doctor.Name,
                   Status = a.Status,
               }).ToList();

          
            ViewBag.patients = patients;
            ViewBag.appointments = appointments;
            ViewBag.Admins = admins;
            return View();
        }
    }
}