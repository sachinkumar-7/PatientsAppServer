using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PatientAppServe.Models;
using PatientsAppServer.Data;

namespace PatientAppServe.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class PharmacyController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public PharmacyController(ApplicationDbContext db)
        {
            _db = db;
        }
        [HttpGet]
        //[Authorize]
        public IActionResult GetPaymentPendingAppointments()
        {
            if (_db.Consultations == null) return Ok();
            var parkedPatients =
                _db.Consultations.Where(m => m.Status == "Payment Pending" && m.Date.Date == DateTime.UtcNow.Date).Include(m=>m.Patients).Include(m=>m.Doctors).ToList();
            return Ok(parkedPatients);

        }
        
        
        [HttpGet("{appointmentId:int}")]
        //[Authorize]
        public IActionResult GetPaymentPendingAppointments(int appointmentId)
        {
            if (_db.Consultations == null) return Ok();
            var parkedPatients =
                _db.Consultations.Where(m => m.Status == "Payment Pending" && m.Date.Date == DateTime.UtcNow.Date && m.AppointmentId == appointmentId).Include(m=>m.Patients).Include(m=>m.Doctors).ToList();
            return Ok(parkedPatients);

        }

        [HttpGet("{doctorId:int}")]
        //[Authorize]
        public IActionResult GetCompletedAppointments(int doctorId)
        {
            if (_db.Consultations == null) return Ok();
            var completedPatients =
                _db.Consultations.Where(m => m.Status == "Completed" && m.Date.Date == DateTime.UtcNow.Date).Include(m=>m.Patients).ToList();
            return Ok(completedPatients);

        }

        [HttpPut]
        public async Task<IActionResult> CompletePatient(Consultation model)
        {
            if (_db.Consultations == null) return Ok();
            var patient = await _db.Consultations.FindAsync(model.AppointmentId);
            if (patient != null)
            {
                patient.Medications = model.Medications;       
                patient.Status = "Completed";
            }

            await _db.SaveChangesAsync();
            return Ok();
        }
    }
}
