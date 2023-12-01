using medical_appointment_scheduling_project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace medical_appointment_scheduling_project.Controllers
{
    public class SpecialistController : Controller
    {
        private dbthucuc db = new dbthucuc();

        [Route("~/specialist")]
        [Route("~/specialist/index")]
        public ActionResult Index()
        {
            var dbClinicAddresses = db.ClinicAddresses.AsQueryable();

            ViewBag.dbClinicAddresses = dbClinicAddresses;
            return View();
        }
        [Route("~/specialist")]
        [Route("~/specialist/respiratory")]
        public ActionResult Respiratory()
        {
            var dbClinicAddresses = db.ClinicAddresses.AsQueryable();

            ViewBag.dbClinicAddresses = dbClinicAddresses;
            return View();
        }
    }
}