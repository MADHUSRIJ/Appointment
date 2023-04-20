using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.Data.SqlClient;

namespace Appointment.Models
{
    public class AppointmentModel
    {
        [Key]
       
        public int AppointmentId { get; set; }

       
        public string? AppointmentTitle { get; set; }

       
        public string? AppointmentDescription { get; set; }

       
        public string? AppointmentType { get; set; }

       
        public string? AppointmentDate { get; set; }

       
        public string? AppointmentTime { get; set; }

       
        public string? Duration { get; set; }

       
        public string? AppointmentStatus { get; set; }

       
        [ForeignKey("USERS")]
        public int UserId { get; set; }

       
        public bool SetReminder { get; set; }

      

      
    }
}
