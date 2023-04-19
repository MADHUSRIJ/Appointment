using Appointment.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Appointment.Controllers
{

    public class AccountController : Controller
    {
        public static UserModel User { get; set; }
        public static AppointmentModel Appointment{ get; set; }
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;

        private readonly SqlConnection sqlConnection;
        public AccountController(IConfiguration configuration)
        {
            sqlConnection = new SqlConnection(configuration.GetConnectionString("AppointmentSchedulerDb"));
            _configuration = configuration;
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(UserModel user)
        {

            if (user.VerifyUser(sqlConnection))
            {
                try
                {
                    User = user;
                    string token = CreateToken();
                    //Response.Headers.Add("Authorization", "Bearer "+ token);
                    Response.Cookies.Append("auth_token", token);
                    return RedirectToAction("Index","Home");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Login Post Controller " + ex.Message);
                }
            }
            return View();
        }

        [HttpPost]
        public string CreateToken()
        {
            try
            {
                List<Claim> claim = new List<Claim>() {
            new Claim(ClaimTypes.Name, User.UserName!),
            new Claim(ClaimTypes.Sid,User.UserId!.ToString()),
            new Claim(ClaimTypes.MobilePhone,User.MobileNumber!),
            new Claim(ClaimTypes.Email,User.Email!),
            };
                var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                    _configuration.GetSection("AppSettings:Token").Value
                    ));
                var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

                var token = new JwtSecurityToken(
                    claims: claim,
                    expires: DateTime.Now.AddHours(2),
                    signingCredentials: cred
                    );
                var jwt = new JwtSecurityTokenHandler().WriteToken(token);
                return jwt;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Create token Controller" + ex.Message);
            }
            return "";

        }

       


        [HttpGet]
        public IActionResult Register()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Register(UserModel user)
        {

            return View();

        }


    }
}
