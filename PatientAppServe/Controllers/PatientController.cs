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
    public class PatientController : ControllerBase
    {

        private readonly ApplicationDbContext _db;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public PatientController(ApplicationDbContext db, UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [HttpGet]

        public async Task<IActionResult> Get()
        {
            return Ok(await _db.Patients.ToListAsync());
        }


        [HttpGet("{patientId}")]

        public async Task<IActionResult> Get(int patientId)
        {
            return Ok(await _db.Patients.FindAsync(patientId));
        }






        [HttpPut]
        public async Task<IActionResult> Update(Patient model)
        {
            var existingPatient = await _db.Patients.FindAsync(model.PatientId);

            if (existingPatient != null)
            {
                existingPatient.Gender = model.Gender;
                existingPatient.EmergencyContactNumber = model.EmergencyContactNumber;
                existingPatient.Pincode = model.Pincode;
                existingPatient.Place = model.Place;
                existingPatient.HouseNo = model.HouseNo;
            }

            await _db.SaveChangesAsync();
            return Ok();
        }



    }
}
