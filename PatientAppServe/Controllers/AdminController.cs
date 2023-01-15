using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PatientAppServe.Models;
using PatientsAppServer.Data;
using System.Reflection;

namespace PatientAppServe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public AdminController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpDelete("Doctors/{id:int}")]
        public async Task<IActionResult> DeleteDoctor(int id)
        {
            var doctor = await _db.Doctors.FindAsync(id);
            if (doctor == null)
                return NotFound();

            _db.Doctors.Remove(doctor);
            await _db.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("DoctorMaster/{id}")]
        public async Task<IActionResult> PutDoctorMaster(Doctor model)
        {
            var existingDoctor = await _db.Doctors.FindAsync(model.Id);

            if (existingDoctor != null)
            {

                existingDoctor.Gender = model.Gender;
                existingDoctor.Dob = model.Dob;
                existingDoctor.FirstName = model.FirstName;
                existingDoctor.LastName = model.LastName;
                existingDoctor.DoctorPhoneNumber = model.DoctorPhoneNumber;
                existingDoctor.Availability = model.Availability;
                existingDoctor.Qualification = model.Qualification;
                existingDoctor.Experience = model.Experience;
            }

            await _db.SaveChangesAsync();
            return Ok();
        }


        [HttpPut("PatientMaster/{id}")]
        public async Task<IActionResult> PutPatientMaster(Patient model)
        {
            var existingPatient = await _db.Patients.FindAsync(model.Id);

            if (existingPatient != null)
            {
                existingPatient.Aadhar = model.Aadhar;
                existingPatient.BloodGroup = model.BloodGroup;
                existingPatient.Dob = model.Dob;
                existingPatient.Gender = model.Gender;
                existingPatient.Pincode = model.Pincode;
                existingPatient.FirstName = model.FirstName;
                existingPatient.Place = model.Place;
                existingPatient.HouseNo = model.HouseNo;
                existingPatient.LastName = model.LastName;
                existingPatient.Relation = model.Relation;
                existingPatient.EmergencyContactNumber = model.EmergencyContactNumber;
                existingPatient.PatientPhoneNumber = model.PatientPhoneNumber;
            }

            await _db.SaveChangesAsync();
            return Ok();
        }



        [HttpGet("Appointments")]
        //[Authorize(Roles = "Doctor")]
        //[Authorize(Roles = "Admin")]
        public IActionResult GetAllAppointments()
        {
            if (_db.Consultations == null) return Ok();
            var AllAppointments =
                _db.Consultations.ToList();

            return Ok(AllAppointments);

        }

    }
}
