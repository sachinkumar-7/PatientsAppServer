using System.ComponentModel.DataAnnotations;

namespace PatientAppServe.Models.ViewModels;

public class DoctorRegisterModel
{
    [Required]
    [StringLength(12, MinimumLength = 10)]
    public string DoctorPhoneNumber { get; set; }

    [Required]
    [StringLength(25, MinimumLength = 1)]
    public string FirstName { get; set; }

    [Required]
    [StringLength(25, MinimumLength = 1)]
    public string LastName { get; set; }

    [Required] public string Gender { get; set; }

    [Required][StringLength(200)] public string Qualification { get; set; }

    [Required] public string Experience { get; set; }

    [Required] public DateTime Dob { get; set; }
    public bool Availability { get; set; }

    public string Specilization { get; set; }
    [Required] public string Email { get; set; }

    [Required] public string Password { get; set; }
    [Compare(nameof(Password))] public string ConfirmPassword { get; set; }
}