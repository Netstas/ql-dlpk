using medical_appointment_scheduling_project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace medical_appointment_scheduling_project.Controllers
{
    public class NewsController : Controller
    {
        private dbthucuc db = new dbthucuc();

        [Route("~/news")]
        [Route("~/news/index")]
        public ActionResult Index()
        {
            var dbClinicAddresses = db.ClinicAddresses.AsQueryable();

            ViewBag.dbClinicAddresses = dbClinicAddresses;
            return View();
        }
        [Route("~/news/bidding")]
        public ActionResult Bidding()
        {
            var dbClinicAddresses = db.ClinicAddresses.AsQueryable();

            ViewBag.dbClinicAddresses = dbClinicAddresses;
            return View();
        }
    }
}