using Appointment.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Reflection.Metadata;
using System.Security.Claims;

namespace Appointment.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public static UserAppointment users { get; set; }
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
                Console.WriteLine("User " + user.UserName);
                Console.WriteLine(user.UserId);
                ViewBag.users = user;
                if (user != null)
                {
                    Console.WriteLine("User " + user.UserName);
                    Console.WriteLine("Current user: ID = " + user.UserId + ", Name = " + user.UserName);
                    // Add code here to set ViewBag properties using the user object
                    user.SetUser(user.UserId);
                    UserAppointment appointmentModel = new UserAppointment();
                    ViewBag.TodayAppointments = user.GetTodayAppointments(user.UserId);
                    ViewBag.UpcomingAppointments = appointmentModel.GetUpcomingAppointments(user.UserId);
                    ViewBag.CompletedAppointments = appointmentModel.GetCompletedAppointments(user.UserId);
                    ViewBag.CancelledAppointments = appointmentModel.GetCancelledAppointments(user.UserId);
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
                    Console.WriteLine("Get curr user " + userid + " name " + username);
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
        public ActionResult UpdateProfile(UserAppointment userAppointment)
        {
            string CurrentPassword = Request.Form["Password"]!;
            string NewPassword = Request.Form["ConfirmPassword"]!;

            UserAppointment appointmentModel = new UserAppointment();

           

            return RedirectToAction("Index", "Home");
        }

        [HttpPost("AddAppointment")]
        public ActionResult AddAppointmeny(UserAppointment userAppointment)
        {
      

            UserAppointment appointmentModel = new UserAppointment();



            return RedirectToAction("Index", "Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}