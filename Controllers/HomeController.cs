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
                
                UserModel user1 = getCurrentUser();
                AppointmentModel appointmentModel = new AppointmentModel();
                ViewBag.TodayAppointments = appointmentModel.GetTodayAppointments(user1.UserId);
                ViewBag.UpcomingAppointments = appointmentModel.GetUpcomingAppointments(user1.UserId);
                ViewBag.CompletedAppointments = appointmentModel.GetCompletedAppointments(user1.UserId);
                ViewBag.CancelledAppointments = appointmentModel.GetCancelledAppointments(user1.UserId);


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

        private UserModel getCurrentUser()
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
                    return new UserModel
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}