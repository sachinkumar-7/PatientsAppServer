using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PatientAppServe.Models
{
    public class Consultation
    {
        [Key]
        public int AppointmentId { get; set; }
        public Patient? Patients { get; set; }
        public int PatientId { get; set; }
        public Doctor? Doctors { get; set; }
        public int DoctorId { get; set; }



        [Column(TypeName="date")]
        public DateTime Date { get; set; }
        public string Session { get; set; }

        public string Status { get; set; }
        public float ConsultationFee { get; set; }

        public string? Diagnosis { get; set; }
        public string? Medications { get; set; }
        public string? Radiology { get; set; }
        public string? LabTest { get; set; }
        public string? Remarks { get; set; }
        public string ConsultationMode { get; set; }
    }
}
