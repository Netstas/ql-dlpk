using medical_appointment_scheduling_project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace medical_appointment_scheduling_project.Controllers
{
    public class AuthController : Controller
    {
        private dbthucuc db = new dbthucuc();

        [Route("~/auth")]
        [Route("~/auth/index")]
        public ActionResult Index()
        {
            ViewBag.SuccessMessage = TempData["SuccessMessage"] as string;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("~/auth/login")]
        public ActionResult Login(string Username, string Password)
        {
            try
            {
                if (!string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password))
                {
                    var admin = db.Admins.FirstOrDefault(a => a.Username == Username && a.Password == Password);

                    if (admin != null)
                    {
                        if (admin.Decentralization == "1")
                        {
                            Session["Name"] = admin.FullName;
                            Session["id"] = admin.Id;
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            ViewBag.ErrorMessage = "Tên đăng nhập hoặc mật khẩu phải là người dùng";
                        }
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
        [Route("~/auth/register")]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("~/auth/registers")]
        public ActionResult Registers(Admin model)
        {
            if (!ModelState.IsValid)
            {
                using (var context = new dbthucuc())
                {
                    var admin = new Admin
                    {
                        Username = model.Username,
                        Password = model.Password,
                        FullName = model.FullName,
                        PhoneNumber = model.PhoneNumber,
                        Email = model.Email,
                        Decentralization = model.Decentralization ?? "1"
                    };

                    context.Admins.Add(admin);
                    context.SaveChanges();
                }
                TempData["SuccessMessage"] = "Tài khoản đã được tạo thành công";
                return RedirectToAction("Index", "Auth");
            }

            return View(model);
        }

        [Route("~/auth/logout")]
        public ActionResult Logout()
        {
            try
            {
                Session.Clear();
                Session.Abandon();

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Đã xảy ra lỗi: " + ex.Message;
                return View("Error");
            }
        }
    }
}