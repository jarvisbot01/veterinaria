namespace Domain.Entities
{
    public class MedicalTreatment : BaseEntity
    {
        public int AppointmentId { get; set; }
        public Appointment Appointment { get; set; }
        public int MedicationId { get; set; }
        public Medication Medication { get; set; }
        public string Dose { get; set; }
        public DateTime AdministrationDate { get; set; }
        public string Observation { get; set; }
    }
}
