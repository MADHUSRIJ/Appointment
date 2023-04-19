using Appointment.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Appointment.Data
{
    public class AppointmentSchedulerDbContext : DbContext
    {
        public AppointmentSchedulerDbContext(DbContextOptions<AppointmentSchedulerDbContext> options) : base(options)
        {
        }

        public DbSet<UserModel> Users { get; set; }
        public DbSet<AppointmentModel> Appointments { get; set; }
    }
}
