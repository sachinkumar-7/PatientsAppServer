using Microsoft.EntityFrameworkCore;
using PatientsAppServer.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PatientAppServe.Models
{
    [Index(nameof(Aadhar), IsUnique = true)]
    public class Patient
    {

        [Key]
        [Required]
        public int PatientId { get; set; }

        public ApplicationUser? UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public string? Id { get; set; }
        public string PatientPhoneNumber { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(20)]
        public string FirstName { get; set; }

        [MaxLength(20)]
        public string LastName { get; set; }
        [Required]
        
        [Column(TypeName="date")]
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
        [Required]
        public string Relation { get; set; }

    }

}

