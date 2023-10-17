namespace Api.Dtos
{
    public class MedicalTreatmentDto
    {
        public int Id { get; set; }
        public int AppointmentId { get; set; }
        public int MedicationId { get; set; }
        public string Dose { get; set; }
        public DateTime AdministrationDate { get; set; }
        public string Observation { get; set; }
    }
}
