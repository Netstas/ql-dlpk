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

    public class AdminController : Controller
    {
        private dbthucuc db = new dbthucuc();

        [Route("~/admin")]
        [Route("~/admin/index")]
        public ActionResult Index()
        {
            return View();
        }


        [Route("~/admin/dashboard")]
        public ActionResult Dashboard(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            var data = db.Appointments.OrderBy(appointment => appointment.OrdinalCode).ToList();

            if (data == null)
            {
                return HttpNotFound("404");
            }

            ViewBag.Data = data.ToPagedList(pageNumber, pageSize);

            return View();
        }

        [Route("~/admin/accountmanagement")]
        public ActionResult AccountManagement(int? page, string searchString)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            var query = db.Admins.AsQueryable(); // Bắt đầu với tất cả dữ liệu.

            if (!string.IsNullOrEmpty(searchString))
            {
                // Lọc theo tên đăng nhập.
                query = query.Where(a => a.PhoneNumber.Contains(searchString));
            }

            var data = query.ToList();
            ViewBag.Data = data.ToPagedList(pageNumber, pageSize);

            return View();
        }

        [Route("~/admin/createaccount")]
        public ActionResult CreateAccount()
        {
            return View();
        }
        [Route("~/admin/schedulemanagement")]
        public ActionResult ScheduleManagement(int? page, string searchString)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            var query = db.Appointments
                 .Include(a => a.Doctor)
                 .OrderBy(a => a.Id)
                 .Select(a => new DoctorViewModel
                 {
                     Id = a.Id,
                     OrdinalCode = a.OrdinalCode,
                     FullName = a.FullName,
                     PhoneNumber = a.PhoneNumber,
                     Status = a.Status,
                     ClinicAddress = a.ClinicAddress.Address,
                     Specialization = a.Specialization.Name,
                     DoctorName = a.Doctor.Name,
                     CreatedAt = a.CreatedAt,
                     BookingDate = a.BookingDate,
                 });

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(a => a.PhoneNumber.Contains(searchString));
            }

            var data = query.ToList();
            ViewBag.Data = data.ToPagedList(pageNumber, pageSize);

            return View();
        }


        [Route("~/admin/editaccount/{id}")]
        public ActionResult EditAccount(int? id)
        {
            var data = db.Admins.FirstOrDefault(a => a.Id == id);

            return View(data);
        }

        [Route("~/admin/createexaminationschedules")]
        public ActionResult CreateExaminationSchedules()
        {
            var doctor = db.Doctor.ToList();
            var user = db.Admins
                .Where(a => a.Decentralization == "1")
                    .ToList();
            var dbClinicAddresses = db.ClinicAddresses.AsQueryable();
            ViewBag.dbClinicAddresses = dbClinicAddresses;
            ViewBag.user = user;
            ViewBag.Doctor = doctor;
            return View();
        }
        [Route("~/admin/eitexaminationschedule/{id}")]
        public ActionResult EditExaminationSchedule(int? id)
        {

            var data = db.Appointments.FirstOrDefault(a => a.Id == id);
            var doctor = db.Doctor.ToList();
            var user = db.Admins
            .Where(a => a.Decentralization == "1")
                .ToList();
            var dbClinicAddresses = db.ClinicAddresses.AsQueryable();
            var dbSpecializations = db.Specializations.AsQueryable();
            var dbDoctors = db.Doctor.AsQueryable();

            ViewBag.dbClinicAddresses = dbClinicAddresses;
            ViewBag.dbSpecializations = dbSpecializations;
            ViewBag.dbDoctors = dbDoctors;
            ViewBag.user = user;
            ViewBag.Doctor = doctor;
            return View(data);
        }
        [Route("~/admin/detailsexaminationschedule/{id}")]
        public ActionResult DetailsExaminationSchedule(int? id)
        {
            var query = db.Appointments
                          .Include(a => a.Doctor)
                          .Where(a => a.Id == id)
                          .OrderBy(a => a.Id)
                          .Select(a => new DoctorViewModel
                          {
                              Id = a.Id,
                              OrdinalCode = a.OrdinalCode,
                              FullName = a.FullName,
                              PhoneNumber = a.PhoneNumber,
                              Syndrome = a.Syndrome,
                              Status = a.Status,
                              BookingDate = a.BookingDate,
                              ClinicAddress = a.ClinicAddress.Address,
                              Specialization = a.Specialization.Name,
                              DoctorName = a.Doctor.Name,
                          })
                          .FirstOrDefault();
            ViewBag.query = query;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("~/admin/login")]
        public ActionResult Login(string Username, string Password)
        {
            try
            {
                if (!string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password))
                {
                    //string hashedPassword = GetSHA256(Password);

                    var admin = db.Admins.FirstOrDefault(a => a.Username == Username && a.Password == Password);

                    if (admin != null)
                    {
                        Session["FullName"] = admin.FullName;
                        Session["AdminIds"] = admin.Id;
                        return RedirectToAction("Dashboard");
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Tên đăng nhập hoặc mật khẩu không đúng.";
                    }
                }
                else
                {
                    ViewBag.ErrorMessage = "Vui lòng nhập tên đăng nhập và mật khẩu.";
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Đã xảy ra lỗi: " + ex.Message;
            }

            return View("Index");
        }

        [Route("~/admin/logout")]
        public ActionResult Logout()
        {
            try
            {
                Session.Clear();
                Session.Abandon();

                return RedirectToAction("Index", "Admin");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Đã xảy ra lỗi: " + ex.Message;
                return View("Error");
            }
        }

        [HttpPost, ValidateInput(false)]
        [Route("~/admin/createaccounts")]
        public ActionResult CreateAccounts(Admin admin)
        {
            try
            {
                using (dbthucuc db = new dbthucuc())
                {
                    if (db.Admins.Any(a => a.Username == admin.Username))
                    {
                        ViewBag.ErrorMessage = "Tên tài khoản không được trùng";
                    }
                    else if (ModelState.IsValid)
                    {
                        db.Admins.Add(admin);
                        db.SaveChanges();

                        return RedirectToAction("AccountManagement");
                    }
                }
                return View("CreateAccount", admin);
            }
            catch
            {
                return HttpNotFound("404");
            }
        }

        [HttpPost, ValidateInput(false)]
        [Route("~/admin/updateaccount")]
        public ActionResult UpdateAccount(Admin updatedAdmin)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(updatedAdmin).State = EntityState.Modified;

                    db.SaveChanges();
                    return RedirectToAction("AccountManagement");
                }

                return View("EditAccount", updatedAdmin);
            }
            catch (Exception ex)
            {
                return HttpNotFound("404");
            }
        }

        [Route("~/admin/deleteadmin/{id}")]
        public ActionResult DeleteAdmin(int? id)
        {
            try
            {
                var admin = db.Admins.FirstOrDefault(a => a.Id == id);
                if (admin == null)
                {
                    return RedirectToAction("AccountManagement");
                }
                db.Admins.Remove(admin);
                db.SaveChanges();
                return RedirectToAction("AccountManagement");
            }
            catch
            {
                return HttpNotFound("404");
            }
        }

        [HttpPost, ValidateInput(false)]
        [Route("~/admin/createexaminationschedule")]
        public ActionResult CreateExaminationSchedule(Appointment appointment)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string uniqueOrdinalCode = GenerateUniqueOrdinalCode();
                    appointment.OrdinalCode = uniqueOrdinalCode;

                    appointment.CreatedAt = DateTime.Now;
                    db.Appointments.Add(appointment);
                    db.SaveChanges();
                    return RedirectToAction("ScheduleManagement");
                }
                return View(appointment);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return View(appointment);
            }

        }
        [HttpPost, ValidateInput(false)]
        [Route("~/admin/updateexaminationschedule")]
        public ActionResult UpdateExaminationSchedule(Appointment updateAppointment)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (var newContext = new dbthucuc())
                    {
                        var existingAppointment = newContext.Appointments.Find(updateAppointment.Id);

                        if (existingAppointment != null)
                        {
                            newContext.Entry(existingAppointment).CurrentValues.SetValues(updateAppointment);
                            newContext.SaveChanges();

                            return RedirectToAction("ScheduleManagement");
                        }
                        else
                        {
                            return HttpNotFound("404");
                        }
                    }

                }

                return View("UpdateExaminationSchedule", updateAppointment);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
                return HttpNotFound("404");
            }
        }

        [Route("~/admin/deleteexaminationschedule/{id}")]
        public ActionResult DeleteExaminationSchedule(int? id)
        {
            try
            {
                var admin = db.Appointments.FirstOrDefault(a => a.Id == id);
                if (admin == null)
                {
                    return RedirectToAction("ScheduleManagement");
                }
                db.Appointments.Remove(admin);
                db.SaveChanges();
                return RedirectToAction("ScheduleManagement");
            }
            catch
            {
                return HttpNotFound("404");
            }
        }

        // Advise

        [Route("~/admin/advise")]
        public ActionResult Advise(int? page, string searchString)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            var query = db.Patients
                 .Include(a => a.Doctor)
                 .OrderBy(a => a.Id)
                 .Select(a => new AdviseViewModel
                 {
                     Id = a.Id,
                     FullName = a.FullName,
                     PhoneNumber = a.PhoneNumber,
                     ClinicAddress = a.ClinicAddress.Address,
                     Specialization = a.Specialization.Name,
                     DoctorName = a.Doctor.Name,
                     Status = a.Status,
                     BookingDate = a.BookingDate

                 });

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(a => a.PhoneNumber.Contains(searchString));
            }
            var data = query.ToList();
            ViewBag.Data = data.ToPagedList(pageNumber, pageSize);
            return View();
        }
        [Route("~/admin/detailsadvise")]
        public ActionResult DetailsAdvise(int id)
        {
            var query = db.Patients
                .Include(a => a.Doctor)
                .Where(a => a.Id == id)
                .OrderBy(a => a.Id)
                .Select(a => new AdviseViewModel
                {
                    Id = a.Id,
                    FullName = a.FullName,
                    PhoneNumber = a.PhoneNumber,
                    Symptoms = a.Symptoms,
                    Status = a.Status,
                    ClinicAddress = a.ClinicAddress.Address,
                    Specialization = a.Specialization.Name,
                    DoctorName = a.Doctor.Name,
                    Date = a.Date,
                    BookingDate = a.BookingDate
                })
                .FirstOrDefault();
            ViewBag.query = query;
            return View();
        }

        [Route("~/admin/createadvise")]
        public ActionResult CreateAdvise()
        {
            var doctor = db.Doctor.ToList();
            var user = db.Admins
            .Where(a => a.Decentralization == "1")
                .ToList();
            var dbClinicAddresses = db.ClinicAddresses.AsQueryable();

            ViewBag.dbClinicAddresses = dbClinicAddresses;
            ViewBag.user = user;
            ViewBag.Doctor = doctor;
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        [Route("~/admin/createadvises")]
        public ActionResult CreateAdvises(Patient patient)
        {
            if (ModelState.IsValid)
            {
                patient.Date = DateTime.Now;
                db.Patients.Add(patient);
                db.SaveChanges();
                return RedirectToAction("Advise");
            }
            return View(patient);
        }
        [Route("~/admin/editadvise/{id}")]
        public ActionResult EditAdvise(int id)
        {
            var patient = db.Patients.Find(id);
            var doctor = db.Doctor.ToList();
            var user = db.Admins
            .Where(a => a.Decentralization == "1")
                .ToList();
            var dbClinicAddresses = db.ClinicAddresses.AsQueryable();

            ViewBag.dbClinicAddresses = dbClinicAddresses;
            ViewBag.user = user;
            ViewBag.Doctor = doctor;
            return View(patient);
        }

        [HttpPost, ValidateInput(false)]
        [Route("~/admin/editadvises")]
        public ActionResult EditAdvises(Patient patient)
        {
            if (ModelState.IsValid)
            {
                db.Entry(patient).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Advise");
            }
            return View(patient);
        }

        [Route("~/admin/Deleteadvise/{id}")]
        public ActionResult Deleteadvise(int? id)
        {
            try
            {
                var admin = db.Patients.FirstOrDefault(a => a.Id == id);
                if (admin == null)
                {
                    return RedirectToAction("Advise");
                }
                db.Patients.Remove(admin);
                db.SaveChanges();
                return RedirectToAction("Advise");
            }
            catch
            {
                return HttpNotFound("404");
            }
        }

        [Route("~/admin/UserManagement")]
        public ActionResult UserManagement(int? page, string searchString)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            var query = db.Admins.AsQueryable(); // Bắt đầu với tất cả dữ liệu.

            if (!string.IsNullOrEmpty(searchString))
            {
                // Lọc theo tên đăng nhập.
                query = query.Where(a => a.PhoneNumber.Contains(searchString));
            }

            var data = query.ToList();
            ViewBag.Data = data.ToPagedList(pageNumber, pageSize);

            return View();
        }
        [Route("~/admin/createuser")]
        public ActionResult CreateUser()
        {
            return View();
        }
        [HttpPost, ValidateInput(false)]
        [Route("~/admin/createusers")]
        public ActionResult CreateUsers(Admin admin)
        {
            try
            {
                using (dbthucuc db = new dbthucuc())
                {
                    if (db.Admins.Any(a => a.Username == admin.Username))
                    {
                        ViewBag.ErrorMessage = "Tên tài khoản không được trùng";
                    }
                    else if (ModelState.IsValid)
                    {
                        db.Admins.Add(admin);
                        db.SaveChanges();

                        return RedirectToAction("UserManagement");
                    }
                }
                return View("CreateAccount", admin);
            }
            catch
            {
                return HttpNotFound("404");
            }
        }
        [Route("~/admin/edituser")]
        public ActionResult EditUser(int id)
        {
            Admin admin = db.Admins.Find(id);

            if (admin == null)
            {
                return HttpNotFound();
            }

            return View(admin);
        }

        [Route("~/admin/editusers")]
        [HttpPost]
        public ActionResult EditUsers(Admin updatedAdmin)
        {
            if (ModelState.IsValid)
            {
                db.Entry(updatedAdmin).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("UserManagement");
            }

            return View(updatedAdmin);
        }
        [Route("~/admin/DeleteUser/{id}")]
        public ActionResult DeleteUser(int? id)
        {
            try
            {
                var admin = db.Admins.FirstOrDefault(a => a.Id == id);
                if (admin == null)
                {
                    return RedirectToAction("UserManagement");
                }
                db.Admins.Remove(admin);
                db.SaveChanges();
                return RedirectToAction("UserManagement");
            }
            catch
            {
                return HttpNotFound("404");
            }
        }

        // quản lý bác sĩ

        [Route("~/admin/DoctorManagement")]
        public ActionResult DoctorManagement(int? page, string searchString)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            var query = db.Doctor.AsQueryable(); // Bắt đầu với tất cả dữ liệu.

            if (!string.IsNullOrEmpty(searchString))
            {
                // Lọc theo tên đăng nhập.
                query = query.Where(a => a.Name.Contains(searchString));
            }

            var data = query.ToList();
            ViewBag.Data = data.ToPagedList(pageNumber, pageSize);

            return View();
        }
        [Route("~/admin/CreateDoctor")]
        public ActionResult CreateDoctor()
        {
            var dbSpecializations = db.Specializations.AsQueryable();
            var dbClinicAddresses = db.ClinicAddresses.AsQueryable();
            ViewBag.dbSpecializations = dbSpecializations;
            ViewBag.dbClinicAddresses = dbClinicAddresses;
            return View();
        }
        [HttpPost, ValidateInput(false)]
        [Route("~/admin/CreateDoctors")]
        public ActionResult CreateDoctors(Doctor doctor)
        {
            try
            {
                using (dbthucuc db = new dbthucuc())
                {
                    if (ModelState.IsValid)
                    {
                        db.Doctor.Add(doctor);
                        db.SaveChanges();

                        return RedirectToAction("DoctorManagement");
                    }
                }
                return View("CreateAccount", doctor);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return HttpNotFound("404");
            }
        }
        [Route("~/admin/EditDoctor")]
        public ActionResult EditDoctor(int id)
        {
            var dbSpecializations = db.Specializations.AsQueryable();
            var dbClinicAddresses = db.ClinicAddresses.AsQueryable();
            ViewBag.dbSpecializations = dbSpecializations;
            ViewBag.dbClinicAddresses = dbClinicAddresses;
            Doctor admin = db.Doctor.Find(id);

            if (admin == null)
            {
                return HttpNotFound();
            }

            return View(admin);
        }

        [Route("~/admin/EditDoctors")]
        [HttpPost]
        public ActionResult EditDoctors(Doctor doctor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(doctor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("DoctorManagement");
            }

            return View(doctor);
        }
        [Route("~/admin/DeleteDoctor/{id}")]
        public ActionResult DeleteDoctor(int? id)
        {
            try
            {
                var admin = db.Doctor.FirstOrDefault(a => a.Id == id);
                if (admin == null)
                {
                    return RedirectToAction("DoctorManagement");
                }
                db.Doctor.Remove(admin);
                db.SaveChanges();
                return RedirectToAction("DoctorManagement");
            }
            catch
            {
                return HttpNotFound("404");
            }
        }

        //ClinicAddressManagement
        [Route("~/admin/ClinicAddressManagement")]
        public ActionResult ClinicAddressManagement(int? page, string searchString)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            var query = db.ClinicAddresses.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(a => a.Address.Contains(searchString));
            }

            var data = query.ToList();
            ViewBag.Data = data.ToPagedList(pageNumber, pageSize);

            return View();
        }
        [Route("~/admin/CreateClinicAddress")]
        public ActionResult CreateClinicAddress()
        {
            return View();
        }
        [HttpPost, ValidateInput(false)]
        [Route("~/admin/CreateClinicAddresss")]
        public ActionResult CreateClinicAddresss(ClinicAddress clinicaddress)
        {
            try
            {
                using (dbthucuc db = new dbthucuc())
                {
                    if (ModelState.IsValid)
                    {
                        db.ClinicAddresses.Add(clinicaddress);
                        db.SaveChanges();

                        return RedirectToAction("ClinicAddressManagement");
                    }
                }
                return View("CreateClinicAddresss", clinicaddress);
            }
            catch
            {
                return HttpNotFound("404");
            }
        }
        [Route("~/admin/EditClinicAddress")]
        public ActionResult EditClinicAddress(int id)
        {
            ClinicAddress admin = db.ClinicAddresses.Find(id);

            if (admin == null)
            {
                return HttpNotFound();
            }

            return View(admin);
        }

        [Route("~/admin/EditClinicAddresss")]
        [HttpPost]
        public ActionResult EditClinicAddresss(ClinicAddress clinicAddress)
        {
            if (ModelState.IsValid)
            {
                db.Entry(clinicAddress).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ClinicAddressManagement");
            }

            return View(clinicAddress);
        }
        [Route("~/admin/DeleteClinicAddresss/{id}")]
        public ActionResult DeleteClinicAddresss(int? id)
        {
            try
            {
                var admin = db.ClinicAddresses.FirstOrDefault(a => a.Id == id);
                if (admin == null)
                {
                    return RedirectToAction("ClinicAddressManagement");
                }
                db.ClinicAddresses.Remove(admin);
                db.SaveChanges();
                return RedirectToAction("ClinicAddressManagement");
            }
            catch
            {
                return HttpNotFound("404");
            }
        }

        //SpecializationManagement

        [Route("~/admin/SpecializationManagement")]
        public ActionResult SpecializationManagement(int? page, string searchString)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            var query = db.Specializations.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(a => a.Name.Contains(searchString));
            }

            var data = query.ToList();
            ViewBag.Data = data.ToPagedList(pageNumber, pageSize);

            return View();
        }
        [Route("~/admin/CreateSpecialization")]
        public ActionResult CreateSpecialization()
        {
            var dbClinicAddress = db.ClinicAddresses.ToList();
            ViewBag.dbClinicAddress = dbClinicAddress;
            return View();
        }
        [HttpPost, ValidateInput(false)]
        [Route("~/admin/CreateSpecializations")]
        public ActionResult CreateSpecializations(Specialization specialization)
        {
            try
            {
                using (dbthucuc db = new dbthucuc())
                {
                    if (db.Specializations.Any(a => a.Name == specialization.Name))
                    {
                        ViewBag.ErrorMessage = "Địa điểm không được trùng";
                    }
                    else if (ModelState.IsValid)
                    {
                        db.Specializations.Add(specialization);
                        db.SaveChanges();

                        return RedirectToAction("SpecializationManagement");
                    }
                }
                return View("CreateSpecializations", specialization);
            }
            catch
            {
                return HttpNotFound("404");
            }
        }
        [Route("~/admin/EditSpecialization")]
        public ActionResult EditSpecialization(int id)
        {
            Specialization admin = db.Specializations.Find(id);
            var dbClinicAddress = db.ClinicAddresses.ToList();
            ViewBag.dbClinicAddress = dbClinicAddress;
            if (admin == null)
            {
                return HttpNotFound();
            }

            return View(admin);
        }

        [Route("~/admin/EditSpecializations")]
        [HttpPost]
        public ActionResult EditSpecializations(Specialization specialization)
        {
            if (ModelState.IsValid)
            {
                db.Entry(specialization).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("SpecializationManagement");
            }

            return View(specialization);
        }
        [Route("~/admin/DeleteSpecializations/{id}")]
        public ActionResult DeleteSpecializations(int? id)
        {
            try
            {
                var admin = db.Specializations.FirstOrDefault(a => a.Id == id);
                if (admin == null)
                {
                    return RedirectToAction("SpecializationManagement");
                }
                db.Specializations.Remove(admin);
                db.SaveChanges();
                return RedirectToAction("SpecializationManagement");
            }
            catch
            {
                return HttpNotFound("404");
            }
        }

        public static string GetSHA256(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = sha256.ComputeHash(bytes);
                StringBuilder builder = new StringBuilder();

                for (int i = 0; i < hashBytes.Length; i++)
                {
                    builder.Append(hashBytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }
        private string GenerateUniqueOrdinalCode()
        {
            var existingOrdinals = db.Appointments
                .Select(a => a.OrdinalCode)
                .ToList()
                .Select(code => int.TryParse(code, out int parsedCode) ? parsedCode : 0);

            int lastUsedNumber = existingOrdinals.DefaultIfEmpty(0).Max();

            if (lastUsedNumber >= 9999999)
            {
                throw new Exception("Số lượng mã OrdinalCode đã đạt giới hạn.");
            }

            string newOrdinalCode = (lastUsedNumber + 1).ToString("D4");

            return newOrdinalCode;
        }
    }
}