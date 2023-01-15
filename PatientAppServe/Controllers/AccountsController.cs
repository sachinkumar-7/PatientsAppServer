using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PatientAppServe.Models;
using PatientAppServe.Models.ViewModels;
using PatientsAppServer.Data;
using PatientsAppServer.Models;

namespace PatientAppServe.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class AccountsController : ControllerBase
{
    private readonly ApplicationDbContext _db;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IConfiguration _configuration;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public AccountsController(ApplicationDbContext db, UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager,IConfiguration configuration)
    {
        _db = db;
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
        _configuration = configuration;
    }

    [HttpPost]
    public async Task<IActionResult> NewPatient(RegisterViewModel model)
    {
        var newPatient = new ApplicationUser
        {
            Email = model.Email,
            PhoneNumber = model.PatientPhoneNumber,
            UserName = Guid.NewGuid().ToString().Replace("-", "").ToLower()
        };

        var result = await _userManager.CreateAsync(newPatient, model.Password);
        if (!result.Succeeded) return Ok("Some Error Has Occured");
        await _userManager.AddToRoleAsync(newPatient, "User");

        await _db.Patients.AddAsync(new Patient
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
            Relation = "Self",
            EmergencyContactNumber = model.EmergencyContactNumber,
            PatientPhoneNumber = model.PatientPhoneNumber,
            Id = _userManager.FindByEmailAsync(model.Email).Result.Id

        });
        await _db.SaveChangesAsync();

        return Ok(
            new
            {
                message = "done"

            });

    }

    [HttpPost]
    public async Task<IActionResult> NewDoctor(DoctorRegisterModel model)
    {

        var newDoctor = new ApplicationUser
        {
            Email = model.Email,
            PhoneNumber = model.DoctorPhoneNumber,
            UserName = Guid.NewGuid().ToString().Replace("-", "").ToLower()
        };
        var res = await _userManager.CreateAsync(newDoctor, model.Password);
        if (!res.Succeeded)
        {
            return BadRequest(new { message = "Some Error Has Occured" });
        }
        await _userManager.AddToRoleAsync(newDoctor, "Doctor");

        await _db.Doctors.AddAsync(new Doctor()
        {
            Dob = model.Dob,
            Gender = model.Gender,
            FirstName = model.FirstName,
            LastName = model.LastName,
            DoctorPhoneNumber = model.DoctorPhoneNumber,
            Id = _userManager.FindByEmailAsync(model.Email).Result.Id,
            Experience = model.Experience,
            Availability = true,
            Qualification = model.Qualification,
            Specilaization = model.Specilization
        });

        await _db.SaveChangesAsync();

        return Ok(new
        {
            message = "done"
        });

    }

    [HttpPost]
    public async Task<IActionResult> GenerateData()
    {
        await _roleManager.CreateAsync(new IdentityRole { Name = "Admin" });
        await _roleManager.CreateAsync(new IdentityRole { Name = "User" });
        await _roleManager.CreateAsync(new IdentityRole { Name = "Doctor" });

        var users = await _userManager.GetUsersInRoleAsync("Admin");
        if (users.Count != 0) return Ok("Data Generated");
        var adminUser = new ApplicationUser
        {
            Email = "admin@123.com",
            UserName = "admin"
        };
        var res = await _userManager.CreateAsync(adminUser, "Pass@123");
        await _userManager.AddToRoleAsync(adminUser, "admin");

        return Ok("Data Generated");
    }

    

    [HttpPost]
    public async Task<IActionResult> Login(LoginModel model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user == null) return BadRequest();

        var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, true);
        if (!result.Succeeded)
            return BadRequest(new
            {
                Success = false,
                Message = "Invalid email / password."
            });

        var token = GenerateToken(user);

        return Ok(new
        {
            Data = token,
            Message = "Login Successful"
        });
    }

    private string GenerateToken(ApplicationUser user)
    {
        var role = _userManager.GetRolesAsync(user).Result.First();
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var docid = "";
        var pid = "";
        try
        {
            pid =  _db.Patients.Where(m=>m.Id == user.Id &&m.Relation=="Self").First().PatientId.ToString();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        try
        {
            docid = _db.Doctors.Where(m=>m.Id == user.Id).First().DoctorId.ToString();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
     
        var UUID = "";
        
        if (role == "Doctor")
        {
            UUID = docid;
        }
        else if(role =="User")
        {
            UUID = pid;
        }
        else if (role =="Pharma")
        {
            UUID = user.Id;
        }
        else
        {
            UUID = user.Id;
        }
    

        //claim is used to add identity to JWT token
        var claims = new[] {
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Sid, user.Id),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(ClaimTypes.Role,role),
            new Claim("Date", DateTime.Now.ToString()),
            new Claim("UUID", UUID)
        };

        var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
            _configuration["Jwt:Audiance"],
            claims,    //null original value
            expires: DateTime.Now.AddMinutes(120),

            //notBefore:
            signingCredentials: credentials);

        var data = new JwtSecurityTokenHandler().WriteToken(token); //return access token 
        return data;
    }
    
    
    
}