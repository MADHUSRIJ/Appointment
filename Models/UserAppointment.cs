using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.Data.SqlClient;

namespace Appointment.Models
{
    public class UserAppointment
    {
        // User Model
        public int UserId { get; set; }


        public string? UserName { get; set; }


        public string? Password { get; set; }

        public string? ConfirmPassword { get; set; }

        public string? Name { get; set; }


        public string? Email { get; set; }

        public string? MobileNumber { get; set; }


        public bool RememberMe { get; set; }


        // Appointments Model
        public int AppointmentId { get; set; }

       
        public string? AppointmentTitle { get; set; }

       
        public string? AppointmentDescription { get; set; }

       
        public string? AppointmentType { get; set; }

       
        public string? AppointmentDate { get; set; }

       
        public string? AppointmentTime { get; set; }

       
        public string? Duration { get; set; }

       
        public string? AppointmentStatus { get; set; }

       
        public bool SetReminder { get; set; }

        public bool SetUser(int UserId)
        {

            try
            {
                using (SqlConnection connection = new SqlConnection("Data Source=5CG9441HWP;Initial Catalog=Appointment Scheduler;Integrated Security=True;Encrypt=False;"))
                {

                    SqlCommand command = new SqlCommand($"SELECT * FROM USERS WHERE UserId = {UserId}", connection);
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        this.UserId = (int)reader["UserId"];
                        this.UserName = (string)reader["UserName"];
                        this.Email = (string)reader["Email"];
                        this.Name = (string)reader["Name"];
                        this.MobileNumber = (string)reader["MobileNumber"];

                        // Erasing the password.
                        this.Password = (string)reader["Password"];
                        return true;
                    }
                }
            }

            catch (SqlException ex)
            {
                Console.WriteLine("Verify User Model " + ex.Message);
            }
            return false;
        }
        public List<AppointmentModel> GetTodayAppointments(int UserId)
        {
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

                       
                        TodayAppointments!.Add(appointmentModel);
                    }
                    reader.Close();
                }
                connect.Close();
               if(TodayAppointments.Count > 0)
                {
                    return TodayAppointments;
                }
                return null;
            }
        }
        public List<AppointmentModel> GetUpcomingAppointments(int UserId)
        {
            
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

                       
                        UpcomingAppointments!.Add(appointmentModel);
                    }
                    reader.Close();
                }
                connect.Close();
                if(UpcomingAppointments.Count > 0)
                {
                    return UpcomingAppointments;
                }
                else
                {
                    return null;
                }
            }
        }
        public List<AppointmentModel> GetCompletedAppointments(int UserId)
        {
           
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

                       
                        CompletedAppointments!.Add(appointmentModel);
                    }
                    reader.Close();
                }
                connect.Close();
                if(CompletedAppointments.Count > 0)
                {
                    return CompletedAppointments;
                }
                else
                {
                    return null;
                }
            }
        }
        public List<AppointmentModel> GetCancelledAppointments(int UserId)
        {
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

                      
                        CancelledAppointments!.Add(appointmentModel);
                    }
                    reader.Close();
                }
                connect.Close();
               if(CancelledAppointments.Count > 0)
                {
                    return CancelledAppointments;
                }
                else
                {
                    return null;
                }
            }
        }
        public bool ChangePassword(int UserId, string NewPassword)
        {
            Console.WriteLine("Model " + UserId);

            //Using stored Procedures
            using (SqlConnection connect = new SqlConnection("Data Source=5CG9441HWP;Initial Catalog=Appointment Scheduler;Integrated Security=True;Encrypt=False;"))
            {
                connect.Open();

                using (SqlCommand command = new SqlCommand("ChangePassword", connect))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add("@UserId", System.Data.SqlDbType.Int).Value = UserId;
                    command.Parameters.Add("@NewPass", System.Data.SqlDbType.NChar).Value = NewPassword;

                    int rowsAffected = command.ExecuteNonQuery();
                   
                    if(rowsAffected > 0)
                    {
                        return true;
                    }
                }
                connect.Close();
                return false;
            }
        }

        public bool CheckPassword(int UserId, string CurrentPassword)
        {
            Console.WriteLine("Model " + UserId);

            //Using stored Procedures
            using (SqlConnection connect = new SqlConnection("Data Source=5CG9441HWP;Initial Catalog=Appointment Scheduler;Integrated Security=True;Encrypt=False;"))
            {
                connect.Open();

                using (SqlCommand command = new SqlCommand("CheckPassword", connect))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add("@UserId", System.Data.SqlDbType.Int).Value = UserId;

                   SqlDataReader reader = command.ExecuteReader();

                   

                    if (reader.Read())
                    {
                        if ((string)reader["Password"] == CurrentPassword)
                        {
                            return true;
                        }
                    }
                }
                connect.Close();
                return false;
            }
        }
    }
}
