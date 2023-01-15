using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PatientAppServe.Models;
using PatientAppServe.Models.ViewModels;
using PatientsAppServer.Data;
using PatientsAppServer.Models;

namespace PatientAppServe.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PatientServiceController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public PatientServiceController(ApplicationDbContext db, UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [HttpGet("{patientId:int}")]
        [Authorize(Roles = "User")]
        public IActionResult GetPendingAppointments(int patientId)
        {
            if (_db.Consultations == null) return Ok();
            var incompleteAppointments =
                _db.Consultations.Where(m => m.PatientId == patientId && m.Status == "Incomplete").Include(m=>m.Doctors);

            return Ok(incompleteAppointments);
        }

        [HttpGet("{id}")]
        public IActionResult GetLinkedAccounts(string id)
        {
            var linkedAccounts = _db.Patients.Where(m => m.Id == id).ToList();
            return Ok(linkedAccounts);

        }

        [HttpGet("{patientId:int}")]
        public IActionResult GetConsultationDetails(int patientId)
        {
            if (_db.Consultations == null) return Ok();
            var completedConsultations =
                _db.Consultations.Where(m => m.PatientId == patientId && m.Status == "Completed").Include(m=>m.Doctors);

            return Ok(completedConsultations);

        }

        [HttpPost]
        public async Task<IActionResult> BookAppointment(Consultation model)
        {
            if (_db.Consultations == null) return Ok();
            await _db.Consultations.AddAsync(new Consultation()
            {
                Date = model.Date,
                Status = "Incomplete",
                PatientId = model.PatientId,
                DoctorId = model.DoctorId,
                ConsultationFee = model.ConsultationFee,
                ConsultationMode = model.ConsultationMode,
                Session = model.Session

            });
            await _db.SaveChangesAsync();
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> AddAssociatedUser(RegisterViewModel model)
        {
            var newPatient = new Patient
            {
                Aadhar = model.Aadhar,
                BloodGroup = model.BloodGroup,
                Dob = model.Dob,
                Gender = model.Gender,
                Pincode = model.Pincode,
                FirstName = model.FirstName,
                Place = model.Place,
                HouseNo = model.HouseNo,
                LastName = model.LastName,
                Relation = model.Relation,
                EmergencyContactNumber = model.EmergencyContactNumber,
                PatientPhoneNumber = _userManager.FindByEmailAsync(model.Email).Result.PhoneNumber,
                Id = _userManager.FindByEmailAsync(model.Email).Result.Id

            };
            await _db.Patients.AddAsync(newPatient);
            await _db.SaveChangesAsync();
            return Ok();
        }

    }
}
