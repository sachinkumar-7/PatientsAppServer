using System.ComponentModel.DataAnnotations;

namespace PatientAppServe.Models
{
    public class PreConsultation
    {
        [Key]
        public long PreConsultationId { get; set; }

        public Consultation Consultations { get; set; }

        public float Weight { get; set; }
        public float Temperature { get; set; }
        public string BloodPressure { get; set; }
        public string BloodSugar { get; set; }
    }
}
