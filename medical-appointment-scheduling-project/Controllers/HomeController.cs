using medical_appointment_scheduling_project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace medical_appointment_scheduling_project.Controllers
{
    
    public class HomeController : Controller
    {
        private dbthucuc db = new dbthucuc();
        [Route("~/")]
        [Route("~/Home")]
        [Route("~/Home/Index")]
        public ActionResult Index()
        {
            ViewBag.SuccessMessage = TempData["SuccessMessage"] as string;
            var appointments = db.Appointments.ToList();
            return View(appointments);
        }
    }
}