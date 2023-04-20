using Appointment.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Diagnostics;
using System.Globalization;
using System.Reflection.Metadata;
using System.Security.Claims;

namespace Appointment.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public static UserAppointment users { get; set; } = new UserAppointment();
        List<AppointmentModel>? TodayAppointments;
        List<AppointmentModel>? UpcomingAppointments;
        List<AppointmentModel>? CompletedAppointments;
        List<AppointmentModel>? CancelledAppointments;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Index()
        {
            try
            {
                
                UserAppointment user = getCurrentUser();
           
               
                if (user != null)
                {
                    users = user;
                    ViewBag.users = user;
                    users.SetUser(users.UserId);
                    UserAppointment appointmentModel = new UserAppointment();
                    ViewBag.TodayAppointments = user.GetTodayAppointments(users.UserId);
                    ViewBag.UpcomingAppointments = appointmentModel.GetUpcomingAppointments(users.UserId);
                    ViewBag.CompletedAppointments = appointmentModel.GetCompletedAppointments(users.UserId);
                    ViewBag.CancelledAppointments = appointmentModel.GetCancelledAppointments(users.UserId);
                }
                else
                {
                    Console.WriteLine("Current user is null");
                }
               
                


            }
            catch (Exception ex)
            {
                Console.WriteLine("Index Controller " + ex.Message);
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        private UserAppointment getCurrentUser()
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;

                string authorizationHeader = HttpContext.Request.Headers["Authorization"];
                if (!string.IsNullOrEmpty(authorizationHeader) && authorizationHeader.StartsWith("Bearer "))
                {
                    string token = authorizationHeader.Substring("Bearer ".Length).Trim();
                    // Do something with the token
                    Console.WriteLine(token);
                }
                if (identity != null)
                {
                    var userClaims = identity.Claims;
                    string userid = (userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Sid)?.Value);
                    string username = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Name)?.Value;
                    Console.WriteLine();
                    return new UserAppointment
                    {
                        UserName = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Name)?.Value,
                        UserId = Convert.ToInt32((userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Sid)?.Value)),
                        MobileNumber = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.MobilePhone)?.Value,
                        Email = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Email)?.Value
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Get current user controller " + ex.Message);
            }
            return null;
        }


        [HttpPost("ChangePassword")]
        public ActionResult ChangePassword(UserAppointment userAppointment)
        {
            string CurrentPassword = Request.Form["Password"]!;
            string NewPassword = Request.Form["ConfirmPassword"]!;

            UserAppointment appointmentModel = new UserAppointment();

            if (appointmentModel.CheckPassword(users.UserId, CurrentPassword))
            {
                if (appointmentModel.ChangePassword(users.UserId, NewPassword))
                {
                    ViewData["Message"] = "Password Changed Sucessfully";
                   
                }
                else
                {
                    ViewData["Message"] = "Check Your New Password";
                }
            }
            else
            {
                Console.WriteLine("else");
                ViewData["Message"] = "Incorrect Old Password";
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost("EditProfile")]
        public ActionResult EditProfile(UserAppointment userAppointment)
        {
            string CurrentPassword = Request.Form["Password"]!;
            string NewPassword = Request.Form["ConfirmPassword"]!;

            UserAppointment appointmentModel = new UserAppointment();

           

            return RedirectToAction("Index", "Home");
        }

        [HttpPost("AddAppointment")]
        public ActionResult AddAppointment(UserAppointment userAppointment)
        {
            
            string GivenDate = Request.Form["AppointmentDate"]!;
            DateTime date = DateTime.ParseExact(GivenDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            string outputDate = date.ToString("dd MMM yyyy");

            string inputTime = Request.Form["AppointmentTime"]!;
            TimeSpan time = TimeSpan.ParseExact(inputTime, "hh\\:mm", CultureInfo.InvariantCulture);
            string outputTime = time.ToString("hh\\:mm\\:ss");

            AppointmentModel appointment = new AppointmentModel();



            appointment.AppointmentTitle = Request.Form["AppointmentTitle"]!;
            appointment.AppointmentDescription = Request.Form["AppointmentDescription"];
            appointment.AppointmentType = Request.Form["AppointmentType"];
            appointment.AppointmentDate = outputDate;
            appointment.AppointmentTime = outputTime;
            appointment.Duration = Request.Form["Duration"];
            appointment.UserId = users.UserId;
            if (Request.Form["SetReminder"] == "false")
            {
                appointment.SetReminder = false;
            }
            else{
                appointment.SetReminder = true;
            }



            UserAppointment appointmentModel = new UserAppointment();
            appointmentModel.AddAppointment(appointment);


            return RedirectToAction("Index", "Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}