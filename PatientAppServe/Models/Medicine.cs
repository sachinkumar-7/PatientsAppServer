using System.ComponentModel.DataAnnotations;

namespace PatientAppServe.Models;

public class Medicine
{
    [Key]
    [Required]
    public int MedicineId { get; set; }

    [Required]
    public string MedicineName { get; set; }

    [Required]
    public string MedicineType { get; set; }
}