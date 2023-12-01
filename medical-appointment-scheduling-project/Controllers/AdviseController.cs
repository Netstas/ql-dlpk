using medical_appointment_scheduling_project.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace medical_appointment_scheduling_project.Controllers
{
    public class AdviseController : Controller
    {
        private dbthucuc db = new dbthucuc();
        [Route("~/advise")]
        public ActionResult _Advise()
        {
            var dbClinicAddresses = db.ClinicAddresses.AsQueryable();

            ViewBag.dbClinicAddresses = dbClinicAddresses;
            return View();

        }
        [HttpPost, ValidateInput(false)]
        [Route("~/advise/put")]
        public ActionResult Put(Patient patient)
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
                        patient.ClinicAddressId = 0;
                        patient.SpecializationId = 0;
                        patient.DoctorId = 0;
                        patient.Status = "0";
                        patient.Date = DateTime.Now;

                        db.Patients.Add(patient);
                        db.SaveChanges();
                        TempData["SuccessMessage"] = "Đặt lịch tư vấn thành công";
                        return RedirectToAction("Index", "Home");

                    }
                    catch (DbEntityValidationException ex)
                    {
                        Console.WriteLine(ex);

                    }
                }
                TempData["SuccessMessage"] = "Đặt lịch tư vấn không thành công";
                return RedirectToAction("Index", "Home"); ;
            }
        }
    }
}