using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PatientAppServe.Models;
using PatientsAppServer.Models;

namespace PatientsAppServer.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<PreConsultation>? PreConsultations { get; set; }
        public DbSet<Consultation>? Consultations { get; set; }
        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    }
}