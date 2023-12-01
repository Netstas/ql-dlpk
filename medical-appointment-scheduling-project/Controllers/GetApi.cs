using medical_appointment_scheduling_project.Models;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Web.Mvc;

namespace medical_appointment_scheduling_project.Controllers
{
    public class GetApiController : Controller
    {
        private readonly dbthucuc db;

        public GetApiController()
        {
            db = new dbthucuc();
        }

        public GetApiController(dbthucuc dbContext)
        {
            db = dbContext;
        }
        [Route("~/api")]
        [Route("~/api/GetSpecializations")]
        [HttpGet]
        public ActionResult GetSpecializations(int clinicAddressId)
        {
            var specializations = db.Specializations
                .Where(s => s.ClinicAddressId == clinicAddressId)
                .Select(s => new { s.Id, s.Name })
                .ToList();

            var jsonResult = JsonConvert.SerializeObject(specializations);
            return Content(jsonResult, "application/json");
        }
        [Route("~/api/GetDoctors")]
        [HttpGet]
        public ActionResult GetDoctors(int specializationId)
        {
            var doctors = db.Doctor
                .Where(d => d.SpecializationId == specializationId)
                .Select(d => new { d.Id, d.Name })
                .ToList();
            var jsonResult = JsonConvert.SerializeObject(doctors);
            return Content(jsonResult, "application/json");

        }
    }
}
