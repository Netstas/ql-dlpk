using medical_appointment_scheduling_project.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace medical_appointment_scheduling_project.Controllers
{
    public class AppointmentController : Controller
    {
        private dbthucuc db = new dbthucuc();
        // GET: Appointment
        [Route("~/appointment")]
        [Route("~/appointment/index")]
        public ActionResult Index()
        {
            var doctor = db.Doctor.ToList();
            var dbClinicAddresses = db.ClinicAddresses.AsQueryable();
            ViewBag.dbClinicAddresses = dbClinicAddresses;
            ViewBag.Doctor = doctor;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("~/appointment/put")]
        public ActionResult Put(Appointment appointment)
        {
            if (Session["id"] == null)
            {
                return RedirectToAction("Index", "Auth");
            }
            else
            {
                if (ModelState.IsValid != null)
                {
                    try
                    {
                        appointment.Status = "0";
                        appointment.CreatedAt = DateTime.Now;

                        db.Appointments.Add(appointment);
                        db.SaveChanges();
                        TempData["SuccessMessage"] = "Đặt lịch khám thành công";
                        return RedirectToAction("Index", "Home");

                    }
                    catch (DbEntityValidationException ex)
                    {
                        Console.WriteLine(ex);

                    }
                }
                TempData["SuccessMessage"] = "Đặt lịch khám không thành công";
                return RedirectToAction("Index", "Home"); ;
            }
        }

    }
}