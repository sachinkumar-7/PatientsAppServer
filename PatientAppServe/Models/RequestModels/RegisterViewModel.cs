using System.ComponentModel.DataAnnotations;

namespace PatientAppServe.Models.ViewModels;

public class RegisterViewModel
{
    [Required] public string Email { get; set; }
    public string? Password { get; set; }
    [Compare(nameof(Password))] public string? ConfirmPassword { get; set; }

    public string? Id { get; set; }
    public string? PatientPhoneNumber { get; set; }

    [Required]
    [MinLength(1)]
    [MaxLength(20)]
    public string FirstName { get; set; }

    [MaxLength(20)]
    public string LastName { get; set; }
    [Required]
    public DateTime Dob { get; set; }
    [Required]
    public string Gender { get; set; }
    [Required]
    [MaxLength(25)]
    public string HouseNo { get; set; }
    [Required]
    [MaxLength(25)]
    public string Place { get; set; }
    [Required]
    public int Pincode { get; set; }
    [Required]
    [MaxLength(10)]
    [MinLength(10)]
    public string EmergencyContactNumber { get; set; }
    public string BloodGroup { get; set; }
    [Required]
    public long Aadhar { get; set; }
    public string Relation { get; set; }
}