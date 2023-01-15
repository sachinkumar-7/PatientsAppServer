using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PatientAppServe.Models;
using PatientsAppServer.Data;
using PatientsAppServer.Models;

namespace PatientAppServe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorServiceController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public DoctorServiceController(ApplicationDbContext db, UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [HttpGet("PendingAppointments/{doctorId:int}")]
        public IActionResult GetPendingAppointments(int doctorId)
        {
            if (_db.Consultations == null) return Ok();
            var pendingAppointments =
                _db.Consultations.Where(m => m.DoctorId == doctorId && m.Status == "Incomplete" && m.Date==DateTime.UtcNow.Date ).Include(m=>m.Patients).ToList();

            
            return Ok(pendingAppointments);

        }


        [HttpGet("ParkedAppointments/{doctorId:int}")]
        public IActionResult GetParkedAppointments(int doctorId)
        {
            if (_db.Consultations == null) return Ok();
            var parkedPatients =
                _db.Consultations.Where(m => m.DoctorId == doctorId && m.Status == "Parked" && m.Date == DateTime.UtcNow.Date).Include(m=>m.Patients).ToList();
            return Ok(parkedPatients);

        }

        // [HttpGet("{patientId:int}")]
        // public IActionResult GetPreviousConsultation(int patientId)
        // {
        //     if (_db.Consultations == null) return Ok();
        //     var previousConsultations =
        //         _db.Consultations.Where(m => m.PatientId == patientId && m.Status == "Completed").ToList();
        //     return Ok(previousConsultations);
        //
        // }

        [HttpPut("Diagnosis")]
        public async Task<IActionResult> AddDiagnosis(Consultation model)
        {
            if (_db.Consultations == null) return Ok();
            var patient = await _db.Consultations.FindAsync(model.AppointmentId);
            if (patient != null)
            {
                patient.Diagnosis = model.Diagnosis;
                patient.Medications = model.Medications;
                patient.Radiology = model.Radiology;
                patient.LabTest = model.LabTest;
                patient.Remarks = model.Remarks;
                patient.Status = "Payment Pending";
            }

            await _db.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("Parked-Patients/{appointmentId:int}")]
        public async Task<IActionResult> ParkPatient(Consultation model, int appointmentId)
        {
            if (_db.Consultations == null) return Ok();
            var patient = await _db.Consultations.FindAsync(appointmentId);
            if (patient != null)
            {
                patient.Diagnosis = model.Diagnosis;
                patient.Medications = model.Radiology;
                patient.LabTest = model.LabTest;
                patient.Remarks = model.Remarks;
                patient.Status = "Parked";
            }

            await _db.SaveChangesAsync();
            return Ok();
        }


    }
}
