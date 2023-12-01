using medical_appointment_scheduling_project.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace medical_appointment_scheduling_project.Controllers
{
    public class MissionVisionController : Controller
    {
        private dbthucuc db = new dbthucuc();

        // GET: MissionVision
        [Route("~/missionvision")]
        [Route("~/missionvision/index")]
        public ActionResult Index()
        {
            var dbClinicAddresses = db.ClinicAddresses.AsQueryable();

            ViewBag.dbClinicAddresses = dbClinicAddresses;
            return View();
        }
    }
}