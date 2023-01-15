using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PatientAppServe.Models;
using PatientsAppServer.Data;

namespace PatientAppServe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DoctorController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Doctor>>> Doctors()
        {
            return await _context.Doctors.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Doctor>> Doctors(int id)
        {
            var doctor = await _context.Doctors.FindAsync(id);

            if (doctor == null)
            {
                return NotFound();
            }

            return doctor;
        }


        [HttpPut]
        [Authorize(Roles = "Admin")]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> PutDoctor(Doctor model)
        {
            var existingDoctor = await _context.Doctors.FindAsync(model.DoctorId);

            if (existingDoctor != null)
            {
                existingDoctor.Gender = model.Gender;
                existingDoctor.DoctorPhoneNumber = model.DoctorPhoneNumber;
                existingDoctor.Availability = model.Availability;
                existingDoctor.Qualification = model.Qualification;
                existingDoctor.Experience = model.Experience;
            }

            await _context.SaveChangesAsync();
            return Ok();
        }





    }
}
