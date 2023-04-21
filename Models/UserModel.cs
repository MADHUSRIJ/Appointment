using Microsoft.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Twilio.Types;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace Appointment.Models
{
    public class UserModel
    {

        [Key]
       
        public int UserId { get; set; }

       
        public string? UserName { get; set; }

       
        public string? Password { get; set; }

       
        public string? Name { get; set; }


        public string? Email { get; set; }

        public string? MobileNumber { get; set; }

       
        public bool RememberMe { get; set; }



        public bool VerifyUser(SqlConnection sqlConnection)
        {


            try
            {
                using (SqlConnection connection = sqlConnection)
                {

                    SqlCommand command = new SqlCommand($"SELECT * FROM USERS WHERE UserName = '{this.UserName}' and Password= '{this.Password}'", connection);
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        this.UserId = (int)reader["UserId"];
                        this.UserName = (string)reader["UserName"];
                        this.Email = (string)reader["Email"];
                        this.Name = (string)reader["Name"];
                        this.MobileNumber = (string)reader["MobileNumber"];
                        this.Name = (string)reader["Name"];


                        Console.WriteLine("Inside Verify User " + this.Name);

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

        public bool RegisterUser(UserModel user)
        {

            //Using stored Procedures
            using (SqlConnection connect = new SqlConnection("Data Source=5CG9441HWP;Initial Catalog=Appointment Scheduler;Integrated Security=True;Encrypt=False;"))
            {
                connect.Open();

                using (SqlCommand command = new SqlCommand("RegisterUser", connect))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add("@UserName", System.Data.SqlDbType.NChar).Value = user.UserName;
                    command.Parameters.Add("@Password", System.Data.SqlDbType.NChar).Value = user.Password;
                    command.Parameters.Add("@Email", System.Data.SqlDbType.NChar).Value = user.Email;
                    command.Parameters.Add("@Name", System.Data.SqlDbType.NChar).Value = user.Name;
                    command.Parameters.Add("@Mobile", System.Data.SqlDbType.NChar).Value = user.MobileNumber;

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        TwilioClient.Init("ACc98ecc30d2270c1fe6b989b31ef9d22e", "8f50c4fd3b4a465581c70d716a519db5");

                        // Construct the SMS message body
                        string messageBody = "You have successfully registered into your Appointy";



                        try
                        {
                            
                            // Send the SMS reminder using the Twilio API
                            MessageResource.Create(
                                            to: new PhoneNumber("+91" + MobileNumber),
                                            from: new PhoneNumber("+16204079346"),
                                            body: messageBody

                                        );
                           

                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Message Create" + ex.Message);
                        }
                        return true;
                    }
                }
                connect.Close();
                return false;
            }
        }


    }
}
