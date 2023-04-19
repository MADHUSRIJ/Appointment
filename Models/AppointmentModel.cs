using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.Data.SqlClient;

namespace Appointment.Models
{
    public class AppointmentModel
    {
        [Key]
        [Required]
        public int AppointmentId { get; set; }

        [Required]
        public string? AppointmentTitle { get; set; }

        [Required]
        public string? AppointmentDescription { get; set; }

        [Required]
        public string? AppointmentType { get; set; }

        [Required]
        public string? AppointmentDate { get; set; }

        [Required]
        public string? AppointmentTime { get; set; }

        [Required]
        public string? Duration { get; set; }

        [Required]
        public string? AppointmentStatus { get; set; }

        [Required]
        [ForeignKey("USERS")]
        public int UserId { get; set; }

        [Required]
        public bool SetReminder { get; set; }

      

        public List<AppointmentModel> GetTodayAppointments(int UserId)
        {
            Console.WriteLine("Model "+UserId);
           List<AppointmentModel>? TodayAppointments = new List<AppointmentModel>();

            //Using stored Procedures
            using (SqlConnection connect = new SqlConnection("Data Source=5CG9441HWP;Initial Catalog=Appointment Scheduler;Integrated Security=True;Encrypt=False;"))
            {
                connect.Open();

                using (SqlCommand command = new SqlCommand("GetTodayAppointments", connect))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add("@UserId", System.Data.SqlDbType.Int).Value = UserId;
                    

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        AppointmentModel appointmentModel = new AppointmentModel();
                        appointmentModel.AppointmentId = (int)reader["AppointmentId"];
                        appointmentModel.AppointmentTitle = reader["AppointmentTitle"].ToString();
                        appointmentModel.AppointmentDescription = reader["AppointmentDescription"].ToString();
                        appointmentModel.AppointmentType = reader["AppointmentType"].ToString();
                        appointmentModel.AppointmentDate = reader["AppointmentDate"].ToString();
                        appointmentModel.AppointmentTime = reader["AppointmentTime"].ToString();
                        appointmentModel.Duration = reader["Duration"].ToString();
                        appointmentModel.AppointmentStatus = reader["AppointmentStatus"].ToString();
                        appointmentModel.UserId = UserId;
                        appointmentModel.SetReminder = (bool)reader["SetReminder"];

                        Console.WriteLine("Desc "+ appointmentModel.AppointmentDescription);

                        TodayAppointments!.Add(appointmentModel);
                    }
                    reader.Close();
                }
                connect.Close();
                return TodayAppointments;
            }
        }
        public List<AppointmentModel> GetUpcomingAppointments(int UserId)
        {
            Console.WriteLine("Model " + UserId);
            List<AppointmentModel>? UpcomingAppointments = new List<AppointmentModel>();

            //Using stored Procedures
            using (SqlConnection connect = new SqlConnection("Data Source=5CG9441HWP;Initial Catalog=Appointment Scheduler;Integrated Security=True;Encrypt=False;"))
            {
                connect.Open();

                using (SqlCommand command = new SqlCommand("GetUpcomingAppointments", connect))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add("@UserId", System.Data.SqlDbType.Int).Value = UserId;


                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        AppointmentModel appointmentModel = new AppointmentModel();
                        appointmentModel.AppointmentId = (int)reader["AppointmentId"];
                        appointmentModel.AppointmentTitle = reader["AppointmentTitle"].ToString();
                        appointmentModel.AppointmentDescription = reader["AppointmentDescription"].ToString();
                        appointmentModel.AppointmentType = reader["AppointmentType"].ToString();
                        appointmentModel.AppointmentDate = reader["AppointmentDate"].ToString();
                        appointmentModel.AppointmentTime = reader["AppointmentTime"].ToString();
                        appointmentModel.Duration = reader["Duration"].ToString();
                        appointmentModel.AppointmentStatus = reader["AppointmentStatus"].ToString();
                        appointmentModel.UserId = UserId;
                        appointmentModel.SetReminder = (bool)reader["SetReminder"];

                        Console.WriteLine("Desc " + appointmentModel.AppointmentDescription);

                        UpcomingAppointments!.Add(appointmentModel);
                    }
                    reader.Close();
                }
                connect.Close();
                return UpcomingAppointments;
            }
        }
        public List<AppointmentModel> GetCompletedAppointments(int UserId)
        {
            Console.WriteLine("Model " + UserId);
            List<AppointmentModel>? CompletedAppointments = new List<AppointmentModel>();

            //Using stored Procedures
            using (SqlConnection connect = new SqlConnection("Data Source=5CG9441HWP;Initial Catalog=Appointment Scheduler;Integrated Security=True;Encrypt=False;"))
            {
                connect.Open();

                using (SqlCommand command = new SqlCommand("GetCompletedAppointments", connect))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add("@UserId", System.Data.SqlDbType.Int).Value = UserId;


                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        AppointmentModel appointmentModel = new AppointmentModel();
                        appointmentModel.AppointmentId = (int)reader["AppointmentId"];
                        appointmentModel.AppointmentTitle = reader["AppointmentTitle"].ToString();
                        appointmentModel.AppointmentDescription = reader["AppointmentDescription"].ToString();
                        appointmentModel.AppointmentType = reader["AppointmentType"].ToString();
                        appointmentModel.AppointmentDate = reader["AppointmentDate"].ToString();
                        appointmentModel.AppointmentTime = reader["AppointmentTime"].ToString();
                        appointmentModel.Duration = reader["Duration"].ToString();
                        appointmentModel.AppointmentStatus = reader["AppointmentStatus"].ToString();
                        appointmentModel.UserId = UserId;
                        appointmentModel.SetReminder = (bool)reader["SetReminder"];

                        Console.WriteLine("Desc " + appointmentModel.AppointmentDescription);

                        CompletedAppointments!.Add(appointmentModel);
                    }
                    reader.Close();
                }
                connect.Close();
                return CompletedAppointments;
            }
        }
        public List<AppointmentModel> GetCancelledAppointments(int UserId)
        {
            Console.WriteLine("Model " + UserId);
            List<AppointmentModel>? CancelledAppointments = new List<AppointmentModel>();

            //Using stored Procedures
            using (SqlConnection connect = new SqlConnection("Data Source=5CG9441HWP;Initial Catalog=Appointment Scheduler;Integrated Security=True;Encrypt=False;"))
            {
                connect.Open();

                using (SqlCommand command = new SqlCommand("GetCancelledAppointments", connect))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add("@UserId", System.Data.SqlDbType.Int).Value = UserId;


                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        AppointmentModel appointmentModel = new AppointmentModel();
                        appointmentModel.AppointmentId = (int)reader["AppointmentId"];
                        appointmentModel.AppointmentTitle = reader["AppointmentTitle"].ToString();
                        appointmentModel.AppointmentDescription = reader["AppointmentDescription"].ToString();
                        appointmentModel.AppointmentType = reader["AppointmentType"].ToString();
                        appointmentModel.AppointmentDate = reader["AppointmentDate"].ToString();
                        appointmentModel.AppointmentTime = reader["AppointmentTime"].ToString();
                        appointmentModel.Duration = reader["Duration"].ToString();
                        appointmentModel.AppointmentStatus = reader["AppointmentStatus"].ToString();
                        appointmentModel.UserId = UserId;
                        appointmentModel.SetReminder = (bool)reader["SetReminder"];

                        Console.WriteLine("Desc " + appointmentModel.AppointmentDescription);

                        CancelledAppointments!.Add(appointmentModel);
                    }
                    reader.Close();
                }
                connect.Close();
                return CancelledAppointments;
            }
        }
    }
}
